# Resposta preparada ao e-mail de 28/10/2026 — não enviada

Assunto sugerido: **Movimentações por mês: divergências e validação do cálculo**

Bom dia, Giovani.

Comparei os três PDFs de SERVIDOR, CANGURU e ESCRITORIO. Das 60 células de vendedor/mês, 51 coincidem e nove divergem. As diferenças também aparecem em meses fechados, mesmo desconsiderando outubro. O relatório do SERVIDOR fica abaixo dos terminais em julho e agosto; por isso não podemos escolhê-lo como referência apenas pelo nome da máquina.

Encontramos um defeito no build analisado da rotina de totalização mensal. Ela soma blocos em paralelo e modifica um array compartilhado ao juntar os resultados. Duas tarefas podem interferir nessa soma. Em um ensaio sintético com entrada fixa, o padrão atual acertou uma de 40 execuções, enquanto a variante que publica um novo array acertou as 40.

Esse mecanismo é compatível com os anexos, mas ainda precisamos confirmar a versão do serviço que executa o relatório de vocês. Os PDFs não demonstram, sozinhos, que as bases eram idênticas ou que não houve alteração retroativa. Portanto ainda não descartamos uma diferença de dados ou sincronização no ambiente.

Somar os maiores valores observados de cada célula dá R$ 1.679.455,08. Esse valor serve para comparar as emissões; não é um total oficial validado e não deve ser usado para fechamento. O total correto precisa de cálculo determinístico sobre os mesmos documentos, itens e filtros.

Para fechar o diagnóstico, precisamos da versão completa do servidor e de um terminal, do destino efetivo da consulta e de uma cópia estável da base. Nessa cópia, o teste é emitir dez vezes o mesmo relatório, com a mesma empresa, usuário, ano, origem e situação, comparando os meses fechados. Depois, conferir cada vendedor/mês contra uma soma serial com os mesmos filtros. Variação na mesma entrada comprova falta de repetibilidade; a identificação do build e a auditoria permitem ligar o sintoma ao defeito encontrado.

A correção do padrão já foi demonstrada no ensaio, mas ainda depende de integração e homologação no build oficial. Nenhum binário instalado foi substituído. Repetir sincronização não corrige a corrida de cálculo; qualquer recuperação de dados deve ser definida apenas se encontrarmos documentos efetivamente ausentes.

Abraço,
Gabriel
