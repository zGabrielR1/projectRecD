// Invoke only the actual private receiver handler, without actor/repository constructors.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DigisatServer.Infrastructure.Data.Core;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

public class CollectionBoundary : RealProxy {
 public readonly List<object> Calls=new();
 public string LastMethod,LastFilter,LastPayload;
 public bool LastOptionsNull,LastUpsert;
 public CollectionBoundary() : base(typeof(IMongoCollection<BsonDocument>)) {}
 public IMongoCollection<BsonDocument> Collection => (IMongoCollection<BsonDocument>)GetTransparentProxy();
 public override IMessage Invoke(IMessage message) {
  var call=(IMethodCallMessage)message;
  try {
   object result;
   switch(call.MethodName) {
    case "GetType": result=typeof(IMongoCollection<BsonDocument>);break;
    case "get_DocumentSerializer":result=BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();break;
    case "get_Settings":result=new MongoCollectionSettings();break;
    case "ReplaceOneAsync":
    case "FindOneAndUpdateAsync":
     LastMethod=call.MethodName;
     LastFilter=((FilterDefinition<BsonDocument>)call.Args[0]).Render(BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>(),BsonSerializer.SerializerRegistry).ToJson();
     LastOptionsNull=call.Args[2]==null;
     LastUpsert=call.Args[2] is UpdateOptions&&((UpdateOptions)call.Args[2]).IsUpsert;
     if(call.MethodName=="ReplaceOneAsync") {
      LastPayload=((BsonDocument)call.Args[1]).ToJson();
      result=Task.FromResult<ReplaceOneResult>(new ReplaceOneResult.Acknowledged(0,0,null));
     } else {
      LastPayload=((UpdateDefinition<BsonDocument>)call.Args[1]).Render(BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>(),BsonSerializer.SerializerRegistry).ToJson();
      result=Task.FromResult<BsonDocument>(null);
     }
     Calls.Add(new{method=LastMethod,filter=LastFilter,payload=LastPayload,optionsNull=LastOptionsNull,upsert=LastUpsert});break;
    default:throw new NotSupportedException("Unexpected collection call: "+call.MethodName);
   }
   return new ReturnMessage(result,null,0,call.LogicalCallContext,call);
  }catch(Exception exception){return new ReturnMessage(exception,call);}
 }
}
public class DatabaseBoundary : RealProxy {
 public readonly CollectionBoundary Target;
 public DatabaseBoundary(CollectionBoundary target) : base(typeof(IMongoDatabase)){Target=target;}
 public IMongoDatabase Database => (IMongoDatabase)GetTransparentProxy();
 public override IMessage Invoke(IMessage message) {
  var call=(IMethodCallMessage)message;
  try {
   object result;
   if(call.MethodName=="GetType")result=typeof(IMongoDatabase);
   else if(call.MethodName=="GetCollection"&&call.Args[0].Equals("rows"))result=Target.Collection;
   else throw new NotSupportedException("Unexpected database call: "+call.MethodName);
   return new ReturnMessage(result,null,0,call.LogicalCallContext,call);
  }catch(Exception exception){return new ReturnMessage(exception,call);}
 }
}
class ReceiverRuntimeProof {
 static readonly List<object> Checks=new();
 static void Check(bool value,string label,object observed){if(!value)throw new Exception("Failed: "+label);Checks.Add(new{label,passed=true,observed});}
 static int Main() {
  var collection=new CollectionBoundary();var database=new DatabaseBoundary(collection);
  var repository=(RepositorySincronizacao)FormatterServices.GetUninitializedObject(typeof(RepositorySincronizacao));
  typeof(RepositorySincronizacao).GetProperty("Database").SetValue(repository,database.Database);
  typeof(RepositorySincronizacao).GetField("_builder",BindingFlags.NonPublic|BindingFlags.Instance).SetValue(repository,Builders<BsonDocument>.Filter);
  var assembly=Assembly.LoadFrom(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"SincronizadorG6.exe"));
  var type=assembly.GetType("Sincronizador.SincronizacaoClientActor",true);
  var actor=FormatterServices.GetUninitializedObject(type);
  type.GetField("_repository",BindingFlags.NonPublic|BindingFlags.Instance).SetValue(actor,repository);
  var method=type.GetMethod("HandleOpLogRequest",BindingFlags.NonPublic|BindingFlags.Instance);
  string id="0123456789abcdef01234567";
  void Case(string name,string payload,string expected) {
   int before=collection.Calls.Count;
   var task=(Task)method.Invoke(actor,new object[]{new OpLogInsertRequest(id,"rows",payload)});
   Check(task.Wait(3000)&&task.Status==TaskStatus.RanToCompletion&&collection.Calls.Count==before+1&&collection.LastMethod==expected,
    name,new{method=collection.LastMethod,filter=collection.LastFilter,payload=collection.LastPayload,optionsNull=collection.LastOptionsNull,upsert=collection.LastUpsert,status=task.Status.ToString()});
   Check(BsonDocument.Parse(collection.LastFilter)["_id"].AsObjectId==ObjectId.Parse(id),name+" uses message ID filter",new{filter=collection.LastFilter});
   Check(expected=="FindOneAndUpdateAsync"?collection.LastOptionsNull:collection.LastUpsert,name+" preserves repository option policy",new{optionsNull=collection.LastOptionsNull,upsert=collection.LastUpsert});
  }
  Case("set modifier","{ '$set': { 'Name': 'fixture' } }","FindOneAndUpdateAsync");
  Case("unset with source marker","{ '$unset': { 'Name': 1 }, '__sourceid__': 'fixture' }","FindOneAndUpdateAsync");
  Case("inc with source marker","{ '$inc': { 'Count': 1 }, '__sourceid__': 'fixture' }","ReplaceOneAsync");
  Case("ordinary replacement","{ 'Memo': 'plain fixture', '__sourceid__': 'fixture' }","ReplaceOneAsync");
  Case("replacement with literal set text","{ 'Memo': 'literal $set text', '__sourceid__': 'fixture' }","FindOneAndUpdateAsync");
  Case("replacement with literal unset text","{ 'Memo': 'literal $unset text', '__sourceid__': 'fixture' }","FindOneAndUpdateAsync");
  Case("literal uppercase SET remains replacement","{ 'Memo': 'literal $SET text' }","ReplaceOneAsync");
  Check(type.GetField("_logger",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(actor)==null,"success path did not require actor logger",new{actorConstructorInvoked=false,repositoryConstructorInvoked=false});
  Console.WriteLine(new JavaScriptSerializer().Serialize(new{actorAssembly=assembly.FullName,actorMvid=assembly.ManifestModule.ModuleVersionId,repositoryMvid=typeof(RepositorySincronizacao).Module.ModuleVersionId,handlerToken="0x"+method.MetadataToken.ToString("x8"),runtime="Native CLR "+Environment.Version,assertions=Checks.Count,checks=Checks,scope="Actual private receiver method and actual repository methods on uninitialized instances; synthetic driver interfaces returning successful zero-match Tasks; no ActorSystem, constructors, entrypoint, config, network or MongoDB"}));
  return 0;
 }
}
