using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
// Actual original and source-compiled pure helpers, synthetic DTOs only.
class RebuiltReportProof {
 static List<object> Checks=new List<object>();
 static void Check(bool ok,string label){if(!ok)throw new Exception(label);Checks.Add(new{label,passed=true});}
 static IEnumerable<Type> Nested(Type t){foreach(var n in t.GetNestedTypes(BindingFlags.Public|BindingFlags.NonPublic)){yield return n;foreach(var x in Nested(n))yield return x;}}
 static decimal[] Allocate(Type service,decimal amount,decimal[] factors){
  var method=service.GetMethod("RatearPorVendedor",BindingFlags.NonPublic|BindingFlags.Static);
  var dto=Activator.CreateInstance(method.GetParameters()[0].ParameterType);dto.GetType().GetProperty("Valor").SetValue(dto,amount,null);
  var rate=service.GetNestedType("RateioVendedorEspecie",BindingFlags.NonPublic);
  var list=(IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(rate));
  foreach(var factor in factors){var r=Activator.CreateInstance(rate,true);rate.GetProperty("Fator").SetValue(r,factor,null);rate.GetProperty("Vendedor").SetValue(r,"Synthetic",null);list.Add(r);}
  return ((IEnumerable)method.Invoke(null,new object[]{dto,list})).Cast<object>().Select(x=>(decimal)x.GetType().GetProperty("Valor").GetValue(x,null)).ToArray();
 }
 static object Exercise(string path){
  var a=Assembly.LoadFrom(path);var service=a.GetType("DigisatServer.Application.Main.AuxiliaresModulo.Services.MovimentacaoSaidaRelatorioAppService",true);
  var full=Allocate(service,10m,new[]{1m/3m,1m/3m,1m/3m});var selected=Allocate(service,10m,new[]{1m/3m});
  Check(full.SequenceEqual(new[]{3.33m,3.33m,3.34m}),"Full thirds allocation "+a.GetName().Name);
  Check(selected.SequenceEqual(new[]{3.33m}),"Single thirds allocation "+a.GetName().Name);
  Check(Allocate(service,-10m,new[]{.5m,.5m}).SequenceEqual(new[]{-5m,-5m}),"Negative payment "+a.GetName().Name);
  Check(Allocate(service,10m,new decimal[0]).Length==0,"Empty rates "+a.GetName().Name);
  var callbacks=Nested(service).SelectMany(t=>t.GetMethods(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.DeclaredOnly)).Where(m=>m.ReturnType==typeof(decimal[])&&m.GetParameters().Select(p=>p.ParameterType).SequenceEqual(new[]{typeof(string),typeof(decimal[])})).ToArray();
  Check(callbacks.Length==3,"Three actual callbacks "+a.GetName().Name);
  foreach(var callback in callbacks){
   var field=callback.DeclaringType.GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance).Single(f=>f.FieldType==typeof(KeyValuePair<string,decimal[]>));
   var closure=FormatterServices.GetUninitializedObject(callback.DeclaringType);field.SetValue(closure,new KeyValuePair<string,decimal[]>("synthetic",Enumerable.Repeat(2m,12).ToArray()));
   var initial=Enumerable.Repeat(10m,12).ToArray();var result=(decimal[])callback.Invoke(closure,new object[]{"synthetic",initial});
   Check(Object.ReferenceEquals(initial,result)&&initial.All(x=>x==12m),"Original mutation retained "+callback.Name+" "+a.GetName().Name);
  }
  return new{assembly=a.FullName,mvid=a.ManifestModule.ModuleVersionId.ToString(),full,selected,callbackCount=callbacks.Length};
 }
 static int Main(string[] args){
  var referenceDirectory=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(args[1])),"references");
  AppDomain.CurrentDomain.AssemblyResolve+=(sender,eventArgs)=>{var path=System.IO.Path.Combine(referenceDirectory,new AssemblyName(eventArgs.Name).Name+".dll");return System.IO.File.Exists(path)?Assembly.LoadFrom(path):null;};
  var original=Exercise(args[0]);var rebuilt=Exercise(args[1]);Console.WriteLine(new JavaScriptSerializer().Serialize(new{runtime=Environment.Version.ToString(),assertions=Checks.Count,checks=Checks,original,rebuilt,scope="Actual original and source-compiled helpers/callbacks; synthetic DTOs, no report service constructor, repositories, configuration, database or application entrypoint"}));return 0;}
}
