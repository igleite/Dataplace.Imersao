using Dataplace.Imersao.Core.Domain.Orcamentos;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;

namespace Dataplace.Imersao.Core.Tests.Fixtures
{
    public class OrcamentoItemFixture
    {
        internal string CdEmpresa = "IMS";
        internal string CdFilial = "01";
        internal int NumOrcamento = 1;
        internal OrcamentoProduto Produto = new OrcamentoProduto(TpRegistroEnum.ProdutoFinal, "100");
        internal decimal Quantidade = 10;
        internal OrcamentoItemPrecoTotal preco = new OrcamentoItemPrecoTotal(10, 15);

        public OrcamentoItem NovoItem()
        {
            return new OrcamentoItem(CdEmpresa, CdFilial, NumOrcamento, Produto, Quantidade, preco);
        }
    }
}