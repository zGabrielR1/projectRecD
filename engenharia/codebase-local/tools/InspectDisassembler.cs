using System;
using System.Linq;
using ICSharpCode.Decompiler.Disassembler;
using ICSharpCode.Decompiler.Metadata;
class InspectDisassembler {
 static void Main() {
  foreach(var t in new[]{typeof(ReflectionDisassembler),typeof(PEFile)}) {
   Console.WriteLine(t.FullName);
   foreach(var c in t.GetConstructors())Console.WriteLine(c);
   foreach(var m in t.GetMethods().Where(m=>m.DeclaringType==t))Console.WriteLine(m);
  }
 }
}
