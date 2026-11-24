// Execute actual Data.Core methods with in-memory driver boundaries only.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using DigisatServer.Infrastructure.Data.Core;
using DigisatServer.Infrastructure.Crosscutting.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

public class MemoryCollection : RealProxy {
 public MemoryCollection() : base(typeof(IMongoCollection<BsonDocument>)) {}
 public override IMessage Invoke(IMessage message) {
  var call=(IMethodCallMessage)message;
  try { return new ReturnMessage(InvokeTarget((MethodInfo)call.MethodBase,call.Args),null,0,call.LogicalCallContext,call); }
  catch(Exception exception) { return new ReturnMessage(exception,call); }
 }
 public Func<FilterDefinition<BsonDocument>,IAsyncCursor<BsonDocument>> Find;
 public int SyncCalls, AsyncCalls;
 public readonly List<string> Filters = new();
 public static (IMongoCollection<BsonDocument>,MemoryCollection) Create() {
  var p=new MemoryCollection(); return((IMongoCollection<BsonDocument>)p.GetTransparentProxy(),p);
 }
 object InvokeTarget(MethodInfo m,object[] args) {
  if(m.Name=="GetType") return typeof(IMongoCollection<BsonDocument>);
  if(m.Name=="get_DocumentSerializer") return BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();
  if(m.Name=="get_Settings") return new MongoCollectionSettings();
  if(m.Name=="FindSync" || m.Name=="FindAsync") {
   if(m.Name=="FindSync") Interlocked.Increment(ref SyncCalls); else Interlocked.Increment(ref AsyncCalls);
   var f=(FilterDefinition<BsonDocument>)args[0];
   lock(Filters) Filters.Add(f.Render(BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>(),BsonSerializer.SerializerRegistry).ToJson());
   var cursor=Find(f);
   return m.Name=="FindSync" ? cursor : Task.FromResult(cursor);
  }
  throw new NotSupportedException("Unexpected database boundary: "+m.Name);
 }
}
public class MemoryCursor : IAsyncCursor<BsonDocument> {
 readonly BsonDocument[] rows; readonly bool failAfterBatch; int step;
 public MemoryCursor(BsonDocument[] rows,bool failAfterBatch=false){this.rows=rows;this.failAfterBatch=failAfterBatch;}
 public IEnumerable<BsonDocument> Current {get; private set;}=Array.Empty<BsonDocument>();
 public bool MoveNext(CancellationToken token=default) {
  token.ThrowIfCancellationRequested();
  if(step++==0){Current=rows;return rows.Length>0;}
  if(failAfterBatch)throw new InvalidOperationException("Synthetic cursor interruption");
  Current=Array.Empty<BsonDocument>();return false;
 }
 public Task<bool> MoveNextAsync(CancellationToken token=default){return Task.FromResult(MoveNext(token));}
 public void Dispose(){}
}
public sealed class MemoryLog : ILoggerFactory,ILogger {
 public int Errors;
 public ILogger Create(){return this;}
 public void Debug(string s,params object[] a){} public void Debug(string s,Exception e,params object[] a){} public void Debug(object i){}
 public void Fatal(string s,params object[] a){} public void Fatal(string s,Exception e,params object[] a){}
 public void LogInfo(string s,params object[] a){} public void LogWarning(string s,params object[] a){}
 public void LogError(string s,params object[] a){Interlocked.Increment(ref Errors);}
 public void LogError(string s,Exception e,params object[] a){Interlocked.Increment(ref Errors);}
}
class SyncRuntimeProof {
 static List<object> results=new();
 static void Check(bool condition,string name,object observed){if(!condition)throw new Exception("Failed: "+name);results.Add(new{name,passed=true,observed});}
 static OpLogTail Tail(IMongoCollection<BsonDocument> oplog,IMongoCollection<BsonDocument> checkpoint) {
  return (OpLogTail)Activator.CreateInstance(typeof(OpLogTail),BindingFlags.NonPublic|BindingFlags.Instance,null,new object[]{oplog,checkpoint,Operations.InsertUpdateAndDelete},CultureInfo.InvariantCulture);
 }
 static BsonDocument Event(int increment) {
  return new BsonDocument{{"op","i"},{"ns","fixture.rows"},{"ts",new BsonTimestamp(1700000000,increment)},{"o",new BsonDocument("_id",increment)}};
 }
 static int Main() {
  var log=new MemoryLog(); LoggerFactory.SetCurrent(log);
  // A checkpoint read fails before the background worker starts.
  var(cp,cpSpy)=MemoryCollection.Create(); cpSpy.Find=_=>throw new InvalidOperationException("Synthetic checkpoint read failure");
  var(op,opSpy)=MemoryCollection.Create();opSpy.Find=_=>throw new Exception("No oplog access allowed in this case");
  using(var tail=Tail(op,cp)) {
   bool thrown=false;try{tail.Run();}catch(InvalidOperationException){thrown=true;}
   Check(thrown&&tail.IsRunning,"checkpoint failure leaves IsRunning true",new{thrown,tail.IsRunning,cpSpy.SyncCalls});
   tail.Run();Check(cpSpy.SyncCalls==1&&opSpy.AsyncCalls==0,"second Run silently skips recovery",new{cpSpy.SyncCalls,opSpy.AsyncCalls});
  }
  // Replay read throws, but configured in-memory logger lets the catch complete.
  var(replay,replaySpy)=MemoryCollection.Create();replaySpy.Find=_=>throw new InvalidOperationException("Synthetic replay read failure");
  var(empty,emptySpy)=MemoryCollection.Create();emptySpy.Find=_=>new MemoryCursor(Array.Empty<BsonDocument>());
  using(var tail=Tail(replay,empty)) {
   int initial=log.Errors;var task=tail.RunBetween(new DateTime(2026,11,1,0,0,0,DateTimeKind.Utc),new DateTime(2026,11,2,0,0,0,DateTimeKind.Utc));
   Check(task.Wait(3000)&&task.Status==TaskStatus.RanToCompletion&&log.Errors==initial+1,"failed replay Task completes successfully",new{status=task.Status.ToString(),errors=log.Errors-initial,replaySpy.AsyncCalls});
  }
  // First cursor delivers two records then throws; second cursor replays them.
  var(stream,streamSpy)=MemoryCollection.Create();int find=0;
  streamSpy.Find=_=>new MemoryCursor(new[]{Event(1),Event(2)},Interlocked.Increment(ref find)==1);
  using(var tail=Tail(stream,empty)) {
   var seen=new List<int>();var done=new ManualResetEventSlim();
   var tokenSource=(CancellationTokenSource)typeof(OpLogTail).GetField("_tokenSource",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(tail);
   tail.Subscribe(x=>{lock(seen){seen.Add(x.Source["ts"].AsBsonTimestamp.Increment);if(seen.Count==4){tokenSource.Cancel();done.Set();}}});
   tail.Run();if(!done.Wait(3000))throw new Exception("Cursor retry did not complete in deadline");
   Thread.Sleep(100);int[] increments;lock(seen)increments=seen.ToArray();
   Check(increments.SequenceEqual(new[]{1,2,1,2})&&streamSpy.AsyncCalls==2,"partial cursor retry duplicates prior events",new{increments,streamSpy.AsyncCalls});
   Check(streamSpy.Filters.Count==2&&streamSpy.Filters[0]==streamSpy.Filters[1],"retry retains pre-failure filter",new{filters=streamSpy.Filters.ToArray()});
   var last=(BsonValue)typeof(OpLogTail).GetField("_lastTs",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(tail);
   Check(last.AsBsonTimestamp.Increment==2,"in-memory lastTs advances despite stale retry filter",new{increment=last.AsBsonTimestamp.Increment});
  }
  Console.WriteLine(new JavaScriptSerializer().Serialize(new{assembly=typeof(OpLogTail).Assembly.FullName,mvid=typeof(OpLogTail).Module.ModuleVersionId,runtime="Native Windows CLR "+Environment.Version+"; "+typeof(object).Assembly.FullName,assertions=results.Count,checks=results,scope="Actual supplied Data.Core assembly; synthetic in-memory IMongoCollection/IAsyncCursor and logger; no database, network, services or config"}));
  return 0;
 }
}
