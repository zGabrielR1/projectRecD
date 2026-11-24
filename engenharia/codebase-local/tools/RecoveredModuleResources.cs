using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using Mono.Cecil;
class RecoveredModuleResources {
 static string Hash(byte[] bytes)=>Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
 static void Main(string[] args) {
  using var module=ModuleDefinition.ReadModule(args[0]);Directory.CreateDirectory(args[1]);
  if(args.Length>2&&args[2]=="--metadata-only") {
   Console.WriteLine(JsonSerializer.Serialize(new{assembly=module.Assembly.Name.FullName,
    references=module.AssemblyReferences.Select(r=>r.Name).ToArray(),
    referenceIdentities=module.AssemblyReferences.Select(r=>r.FullName).ToArray()},new JsonSerializerOptions{WriteIndented=true}));
   return;
  }
  int index=0;
  var resources=module.Resources.OfType<EmbeddedResource>().Select(r=>{
   var bytes=r.GetResourceData();var name=(index++).ToString("D4")+".bin";
   File.WriteAllBytes(Path.Combine(args[1],name),bytes);
   return new{name=r.Name,file=name,bytes=bytes.Length,sha256=Hash(bytes)};
  }).ToArray();
  Console.WriteLine(JsonSerializer.Serialize(new{artifactSha256=Hash(File.ReadAllBytes(args[0])),mvid=module.Mvid,
   assembly=module.Assembly.Name.FullName,entryPoint=module.EntryPoint?.DeclaringType.FullName,
   references=module.AssemblyReferences.Select(r=>r.Name).ToArray(),resources},new JsonSerializerOptions{WriteIndented=true}));
 }
}
