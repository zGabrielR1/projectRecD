// Actual actor-bound CollectionReplicator methods; recipients/checkpoints in memory.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Web.Script.Serialization;
using Akka.Actor;
using DigisatServer.Infrastructure.Data.Core;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

public class CheckpointSpy : RealProxy {
 public readonly List<BsonDocument> Writes=new();
 public CheckpointSpy() : base(typeof(IMongoCollection<BsonDocument>)) {}
 public IMongoCollection<BsonDocument> Collection => (IMongoCollection<BsonDocument>)GetTransparentProxy();
 public override IMessage Invoke(IMessage message) {
  var call=(IMethodCallMessage)message;
  try {
   object result;
   switch(call.MethodName) {
    case "GetType":result=typeof(IMongoCollection<BsonDocument>);break;
    case "get_DocumentSerializer":result=BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();break;
    case "get_Settings":result=new MongoCollectionSettings();break;
    case "ReplaceOne":
     var filter=((FilterDefinition<BsonDocument>)call.Args[0]).Render(BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>(),BsonSerializer.SerializerRegistry);
     var data=(BsonDocument)call.Args[1];
     if(!filter.Contains("_id")||!data.Contains("ts"))throw new Exception("Unexpected checkpoint shape");
     Writes.Add(data.DeepClone().AsBsonDocument);
     result=new ReplaceOneResult.Acknowledged(1,1,null);break;
    default:throw new NotSupportedException("Unexpected collection boundary: "+call.MethodName);
   }
   return new ReturnMessage(result,null,0,call.LogicalCallContext,call);
  }catch(Exception e){return new ReturnMessage(e,call);}
 }
}
public class Recipient : ICanTell {
 public readonly List<OpLogInsertRequest> Messages=new();
 public bool Reject;
 public void Tell(object message,IActorRef sender) {
  if(Reject)throw new InvalidOperationException("Synthetic recipient failure");
  var request=message as OpLogInsertRequest;if(request==null)throw new Exception("Unexpected message type");
  Messages.Add(request);
 }
}
class ReplicationRuntimeProof {
 static readonly ObjectId Id=ObjectId.Parse("0123456789abcdef01234567");
 static readonly List<object> Checks=new();
 static void Check(bool value,string label,object observed){if(!value)throw new Exception("Failed: "+label);Checks.Add(new{label,passed=true,observed});}
 static OpLogTail Tail(CheckpointSpy checkpoint) => (OpLogTail)Activator.CreateInstance(typeof(OpLogTail),BindingFlags.NonPublic|BindingFlags.Instance,null,new object[]{null,checkpoint.Collection,Operations.InsertUpdateAndDelete},CultureInfo.InvariantCulture);
 static MongoOperation Operation(string op,BsonDocument body,int increment,string ns="fixture.rows") {
  var source=new BsonDocument{{"op",op},{"ns",ns},{"ts",new BsonTimestamp(1700000000,increment)},{"o",body}};
  if(op=="u")source["o2"]=new BsonDocument("_id",Id);
  return (MongoOperation)Activator.CreateInstance(typeof(MongoOperation),BindingFlags.NonPublic|BindingFlags.Instance,null,new object[]{source},CultureInfo.InvariantCulture);
 }
 static void Emit(OpLogTail tail,MongoOperation operation) => ((IObserver<MongoOperation>)typeof(OpLogTail).GetField("_subject",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(tail)).OnNext(operation);
 static BsonDocument Body(Recipient recipient) => BsonDocument.Parse(recipient.Messages.Last().Data);
 static int Main() {
  var checkpoint=new CheckpointSpy();var recipient=new Recipient();
  using(var tail=Tail(checkpoint)) {
   CollectionReplicator.From("source-fixture",tail,"fixture","rows").To(new ICanTell[]{recipient},null);
   var insert=new BsonDocument{{"_id",Id},{"Name","fixture"}};
   Emit(tail,Operation("i",insert,1));
   Check(recipient.Messages.Count==1&&recipient.Messages[0].Id==Id.ToString()&&recipient.Messages[0].CollectionAffected=="rows","insert emits original DTO coordinates",new{recipient.Messages[0].Id,recipient.Messages[0].CollectionAffected});
   Check(Body(recipient)["__sourceid__"]=="source-fixture"&&!insert.Contains("__sourceid__"),"insert adds source marker only to clone",new{outgoing=Body(recipient).ToJson(),original=insert.ToJson()});
   Check(checkpoint.Writes.Count==1&&checkpoint.Writes[0]["ts"].AsBsonTimestamp.Increment==1,"checkpoint advances after Tell without recipient processing",new{writes=checkpoint.Writes.Count,recipientProcessingPerformed=false});
   Emit(tail,Operation("u",new BsonDocument("$set",new BsonDocument("Name","updated")),2));
   Check(!Body(recipient).Contains("__sourceid__")&&Body(recipient).Contains("$set"),"set modifier omits source marker",new{outgoing=Body(recipient).ToJson()});
   Emit(tail,Operation("u",new BsonDocument("$unset",new BsonDocument("Name",1)),3));
   Check(Body(recipient).Contains("__sourceid__")&&Body(recipient).Contains("$unset"),"unset modifier receives top-level source marker",new{outgoing=Body(recipient).ToJson()});
   Emit(tail,Operation("u",new BsonDocument("$inc",new BsonDocument("Count",1)),4));
   Check(Body(recipient).Contains("__sourceid__")&&Body(recipient).Contains("$inc"),"inc modifier receives top-level source marker",new{outgoing=Body(recipient).ToJson()});
   Emit(tail,Operation("u",new BsonDocument{{"_id",Id},{"Memo","literal $set text"}},5));
   Check(Body(recipient).Contains("__sourceid__")&&!Body(recipient).Contains("$set")&&recipient.Messages.Last().Data.Contains("$set"),"replacement literal differs from BSON key classification",new{outgoing=Body(recipient).ToJson(),containsSetText=recipient.Messages.Last().Data.Contains("$set"),hasSetKey=Body(recipient).Contains("$set")});
   int messages=recipient.Messages.Count;Emit(tail,Operation("d",new BsonDocument("_id",Id),6));
   Check(recipient.Messages.Count==messages&&checkpoint.Writes.Last()["ts"].AsBsonTimestamp.Increment==6,"physical delete emits no message but advances checkpoint",new{newMessages=recipient.Messages.Count-messages,checkpointIncrement=6});
   int writes=checkpoint.Writes.Count;Emit(tail,Operation("i",new BsonDocument("_id",Id),7,"other.rows"));
   Check(recipient.Messages.Count==messages&&checkpoint.Writes.Count==writes,"namespace filtering excludes foreign database",new{newMessages=recipient.Messages.Count-messages,newWrites=checkpoint.Writes.Count-writes});
  }
  var emptyCheckpoint=new CheckpointSpy();using(var tail=Tail(emptyCheckpoint)) {
   CollectionReplicator.From("source-fixture",tail,"fixture","rows").To(Array.Empty<ICanTell>(),null);
   Emit(tail,Operation("i",new BsonDocument("_id",Id),8));
   Check(emptyCheckpoint.Writes.Count==1,"empty recipient list still advances checkpoint",new{writes=emptyCheckpoint.Writes.Count,recipientCount=0});
  }
  var disabledCheckpoint=new CheckpointSpy();var disabledRecipient=new Recipient();using(var tail=Tail(disabledCheckpoint)) {
   CollectionReplicator.From("source-fixture",tail,"fixture","rows").To(new ICanTell[]{disabledRecipient},null,null,false);
   Emit(tail,Operation("i",new BsonDocument("_id",Id),9));
   Check(disabledRecipient.Messages.Count==1&&disabledCheckpoint.Writes.Count==0,"updateLastTs false sends without checkpoint",new{messages=disabledRecipient.Messages.Count,writes=disabledCheckpoint.Writes.Count});
  }
  var echoCheckpoint=new CheckpointSpy();var echoRecipient=new Recipient();using(var tail=Tail(echoCheckpoint)) {
   CollectionReplicator.From("source-fixture",tail,"fixture","rows").To(new ICanTell[]{echoRecipient},null,"target-fixture");
   Emit(tail,Operation("i",new BsonDocument{{"_id",Id},{"__sourceid__","target-fixture"}},10));
   Check(echoRecipient.Messages.Count==0&&echoCheckpoint.Writes.Count==0,"echo suppression returns before checkpoint",new{messages=echoRecipient.Messages.Count,writes=echoCheckpoint.Writes.Count});
  }
  var failureCheckpoint=new CheckpointSpy();var first=new Recipient();var second=new Recipient{Reject=true};using(var tail=Tail(failureCheckpoint)) {
   CollectionReplicator.From("source-fixture",tail,"fixture","rows").To(new ICanTell[]{first,second},null);
   bool failed=false;try{Emit(tail,Operation("i",new BsonDocument("_id",Id),11));}catch(InvalidOperationException){failed=true;}
   Check(failed&&first.Messages.Count==1&&failureCheckpoint.Writes.Count==0,"recipient throw leaves partial send without checkpoint",new{failed,firstRecipientMessages=first.Messages.Count,writes=failureCheckpoint.Writes.Count});
  }
  Console.WriteLine(new JavaScriptSerializer().Serialize(new{assembly=typeof(CollectionReplicator).Assembly.FullName,mvid=typeof(CollectionReplicator).Module.ModuleVersionId,runtime="Native CLR "+Environment.Version,assertions=Checks.Count,checks=Checks,scope="Actual original actor-bound emitter with synthetic ICanTell and checkpoint; no ActorSystem, MongoDB, services, network or customer config"}));
  return 0;
 }
}
