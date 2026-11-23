using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000184 RID: 388
	public class DetalheSegmentoWRetornoCNAB240
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00070BCC File Offset: 0x0006EDCC
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00070BD4 File Offset: 0x0006EDD4
		public int CodigoErro
		{
			get
			{
				return this._codigoErro;
			}
			set
			{
				this._codigoErro = value;
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00070BE0 File Offset: 0x0006EDE0
		public void LerDetalheSegmentoWRetornoCNAB240(string registro)
		{
			try
			{
				if (registro.Substring(13, 1) != "W")
				{
					throw new Exception("Registro inválido. O detalhe não possuí as características do segmento W.");
				}
				if (!registro.Substring(28, 3).Trim().Equals(""))
				{
					this._codigoErro = Convert.ToInt32(registro.Substring(28, 3));
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO W.", ex);
			}
		}

		// Token: 0x04000707 RID: 1799
		private int _codigoErro;
	}
}
