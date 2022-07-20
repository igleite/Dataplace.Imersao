using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Tests.Fixtures;
using Xunit;

namespace Dataplace.Imersao.Core.Tests.Domain.Orcamentos
{
    [Collection(nameof(OrcamentoCollection))]
    public class OrcamentoItemTest
    {
        private readonly OrcamentoFixture _fixture;
        private readonly OrcamentoItemFixture _itemFixture;

        public OrcamentoItemTest(OrcamentoFixture fixture, OrcamentoItemFixture itemFixture)
        {
            _fixture = fixture;
            _itemFixture = itemFixture;
        }

        [Fact]
        public void FecharOrcamentoItemDeveRetornarStatusFechado()
        {
            //arrange
            var item = _itemFixture.NovoItem();

            //act
            item.FecharOrcamento();

            //assert
            Assert.Equal(OrcamentoItemStatusEnum.Cancelado, item.Situacao);
        }
    }
}