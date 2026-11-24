# Consolidação de achados RE e caso Giovani — 03/11/2026

Continuação: [investigação REA de 11/11/2026](rea-investigation-2026-11-11.md), com identidade atual dos DLLs, rateio por vendedor e diferenças do mensal por produto; [revisão adicional de sincronização](rea-sync-review-2026-11-11.md), vinculada às fontes históricas.

Verificações posteriores executadas: [runtime dos callbacks/rateio atuais](runtime-reports-2026-11-11.md) e [runtime da sincronização atual/histórica](runtime-sync-2026-11-11.md), usando entradas sintéticas e os métodos originais dos DLLs.

O [índice de runtime de 11/11](runtime-investigation-index-2026-11-11.md) reúne as rodadas, incluindo rateio desde os itens e emissor de replicação, com comandos e lacunas restantes.

Este índice reúne evidências já produzidas; não representa nova execução dos binários nem homologação do ambiente remoto.

| Achado | Evidência e alcance | Pendência |
| --- | --- | --- |
| Mensal por vendedor modifica arrays compartilhados dentro de `ConcurrentDictionary.AddOrUpdate` | [Diagnóstico e reprodução](diagnostico-sincronizacao-e-mensal.md): build local, token `0x0600328a`, CIL normalizado `eb8ff402138693715a1af3ab25f4166aaf48f50ded5553a1ab767e0b8ae0f74e`; padrão original correto em 1/40 execuções sintéticas, novo array em 40/40 | Confirmar hash do assembly carregado no servidor de Giovani e reconciliar dados congelados |
| Outros totalizadores usam o mesmo padrão | Registro `insight_upstream_issues.py`, assembly `DigisatServer.Application.Main.dll`, SHA-256 `471908afa8e8ae43aaab13722e7820006c4b7a221600983abc42e9c3b9039af4` | Homologar cada agrupamento; aviso no Insight é candidato, não prova de erro naquela consulta |
| Preço unitário com quantidade fracionada | [Auditorias independentes](auditorias-independentes.md), rota `/api/audit/unit-prices`, condição de agrupamento por preço desativado | Auditoria opera sobre entrada explícita; não captura nem corrige automaticamente o servidor |
| Sincronização não equivale a espelhar todo o histórico | [Diagnóstico](diagnostico-sincronizacao-e-mensal.md): carga servidor→terminal é de cadastros; checkpoint/entrega têm limites de confirmação | Identificar botão usado e comparar IDs reais antes de recuperação |
| Defeitos de delete e checkpoint reproduzidos | [Protótipo](bugs-sincronizacao-prototipo-2026-10-30.md) e [Data.Core recompilado](recompilacao-data-core-2026-10-30.md) | Reconstrução preserva comportamento original; protótipo corrigido é separado e não foi implantado |
| Bibliotecas reconstruídas de C# | [Data.Core](recompilacao-data-core-2026-10-30.md), [relatórios](recompilacao-relatorios-2026-10-30.md), [auditoria](validacao-reconstrucao-e-auditoria-independente.md) | Compilação e testes selecionados não demonstram equivalência integral ou suporte em produção |

## Bug para suporte: Giovani — “Movimentações por mês”

**Sintoma:** três PDFs de 28/10/2026, mesma empresa/ano/origem/situação impressos, têm 9 células divergentes entre 60 células vendedor/mês; 51 coincidem. Há diferenças em meses fechados. Outubro deve ser excluído do teste principal por estar em andamento na emissão. [Comparação detalhada](analise-relatorios-mes-2026-10-28.md).

**Esperado:** em uma entrada congelada, o mesmo conjunto de filtros e empresa deve produzir exatamente os mesmos valores por vendedor/mês em todas as execuções e pontos de acesso ao mesmo serviço.

**Defeito demonstrado no build analisado:** o callback soma em `existing[i]` do array compartilhado. A proteção do dicionário não protege essa mutação; callbacks podem ser reexecutados. Publicar um novo array sem modificar `existing` ou reduzir partições em série remove esse padrão de corrida. O ensaio sintético demonstra o defeito e a correção do padrão, sem executar o banco do cliente.

**Diagnóstico do caso:** compatível com o defeito, ainda sem causa confirmada no servidor remoto. Divergências entre PDFs não demonstram igualdade das bases, mesmo endpoint ou ausência de alterações retroativas. Comparar cada célula com o máximo observado é uma descrição dos anexos, não prova de que só houve perda. R$ 1.679.455,08 é a soma desses máximos, não total oficial; seu uso como piso depende da hipótese ainda não confirmada de perdas sobre a mesma entrada positiva.

**Reprodução solicitada ao suporte:** identificar versão/hash do serviço efetivo, empresa, usuário e filtros; congelar uma cópia; emitir dez vezes no mesmo endpoint; comparar cada célula com redução serial dos itens usando os filtros exatos. Incluir mais de 500 documentos, múltiplos vendedores, cancelamentos/descontos e graus 1, 2, 3 e 8. Se houver divergência documental entre bases, investigar sincronização separadamente.

**Estado:** relatório do bug e resposta ao Giovani preparados neste repositório. Nenhum e-mail/ticket foi enviado, nenhum binário do fornecedor foi substituído e não há publicação oficial confirmada. [Resposta pronta para revisão](resposta-email-divergencia-servidor-terminais.md).
