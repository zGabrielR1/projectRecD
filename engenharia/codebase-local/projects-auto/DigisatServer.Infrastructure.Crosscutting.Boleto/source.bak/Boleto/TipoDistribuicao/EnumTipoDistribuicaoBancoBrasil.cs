using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000052 RID: 82
	public enum EnumTipoDistribuicaoBancoBrasil
	{
		// Token: 0x0400019F RID: 415
		[Descricao("Banco distribui")]
		BancoDistribui = 1,
		// Token: 0x040001A0 RID: 416
		[Descricao("Cliente distribui")]
		ClienteDistribui,
		// Token: 0x040001A1 RID: 417
		[Descricao("Banco envia e-mail")]
		BancoEnviaEmail
	}
}
