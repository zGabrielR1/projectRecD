using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Disassembler;
using ICSharpCode.Decompiler.Metadata;
using Mono.Cecil;

// File-backed disassembly only: no CLR load or target code execution.
class ManagedIlWorkspace {
 static int Main(string[] args) {
  if(args.Length!=2)throw new ArgumentException("input assembly, new output directory");
  var directory=Path.GetFullPath(args[1]);Directory.CreateDirectory(directory);
  var output=Path.Combine(directory,Path.GetFileNameWithoutExtension(args[0])+".il");
  using(var pe=new PEFile(args[0],PEStreamOptions.PrefetchEntireImage,MetadataReaderOptions.None))
  using(var writer=new StreamWriter(output,false,new UTF8Encoding(false))) {
   var dis=new ReflectionDisassembler(new PlainTextOutput(writer),CancellationToken.None);
   dis.DetectControlStructure=false;
   dis.ShowMetadataTokens=true;
   dis.WriteAssemblyReferences(pe.Metadata);
   dis.WriteAssemblyHeader(pe);
   dis.WriteModuleHeader(pe,false);
   dis.WriteModuleContents(pe);
  }
  using(var module=Mono.Cecil.ModuleDefinition.ReadModule(args[0])) {
   int resources=0;
   foreach(var resource in module.Resources) {
    var embedded=resource as EmbeddedResource;if(embedded==null)continue;
    var file=Path.GetFullPath(Path.Combine(directory,resource.Name));
    if(!file.StartsWith(directory+Path.DirectorySeparatorChar,StringComparison.OrdinalIgnoreCase))throw new Exception("Resource path outside output");
    Directory.CreateDirectory(Path.GetDirectoryName(file));File.WriteAllBytes(file,embedded.GetResourceData());resources++;
   }
   Console.WriteLine("Disassembled "+module.Name+"; resources="+resources+"; IL bytes="+new FileInfo(output).Length);
  }
  return 0;
 }
}
