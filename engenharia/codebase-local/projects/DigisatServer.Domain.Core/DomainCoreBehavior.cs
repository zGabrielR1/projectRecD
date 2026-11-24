using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using DigisatServer.Domain.Core;
using MongoDB.Bson;
class SyntheticEntity : Entity {
 public SyntheticEntity(ObjectId id) : base(true,TimeSpan.FromTicks(10),id) {}
 public void AddSearch(string value){AdicionarInformacaoPesquisa(value);}
 public void RemoveSearch(string value){RemoverInformacaoPesquisa(value);}
 public void ClearSearch(){LimparInformacoesPesquisa();}
}
class DomainCoreBehavior {
 static List<object> checks=new List<object>();
 static void Check(bool ok,string name){if(!ok)throw new Exception(name);checks.Add(new{name,passed=true});}
 static void Main() {
  var id=ObjectId.Parse("0123456789abcdef01234567");var other=ObjectId.Parse("1123456789abcdef01234567");
  var row=new SyntheticEntity(id);
  Check(row.Id==id&&row.Ativo&&row.Versao.Ticks==10,"Explicit constructor sets id/state/version");
  row.Desativar();Check(!row.Ativo,"Deactivate");row.Ativar();Check(row.Ativo,"Activate");
  row.AtualizarVersao(TimeSpan.FromTicks(20));Check(row.Versao.Ticks==20,"Explicit version update");
  row.TestarVersao(null);Check(true,"Null comparison version allowed");
  var equal=new SyntheticEntity(other);equal.AtualizarVersao(TimeSpan.FromTicks(20));row.TestarVersao(equal);Check(true,"Equal version allowed");
  bool mismatch=false;equal.AtualizarVersao(TimeSpan.FromTicks(21));try{row.TestarVersao(equal);}catch(Exception){mismatch=true;}Check(mismatch,"Different version throws");
  Check(EntityBaseSpecification<SyntheticEntity>.PorId(id).SatisfiedBy().Compile()(row),"ObjectId specification positive");
  Check(!EntityBaseSpecification<SyntheticEntity>.PorId(other).SatisfiedBy().Compile()(row),"ObjectId specification negative");
  Check(EntityBaseSpecification<SyntheticEntity>.PorId(id.ToString()).SatisfiedBy().Compile()(row),"String id specification");
  row.AddSearch(" ÁRVORE ");row.AddSearch(" ÁRVORE ");Check(row.InformacoesPesquisa.Count==1,"Search normalization deduplicates");
  Check(EntityBaseSpecification<SyntheticEntity>.PorInformacaoPesquisa(" ÁRVORE ").SatisfiedBy().Compile()(row),"Search specification normalized value");
  row.ClearSearch();Check(row.InformacoesPesquisa.Count==0,"Clear search");
  Console.WriteLine(new JavaScriptSerializer().Serialize(new{assembly=typeof(Entity).Assembly.FullName,mvid=typeof(Entity).Module.ModuleVersionId,runtime=Environment.Version.ToString(),assertions=checks.Count,checks,scope="Actual domain constructors/properties/specifications on explicit synthetic entity; no database/service/application entrypoint/configuration."}));
 }
}
