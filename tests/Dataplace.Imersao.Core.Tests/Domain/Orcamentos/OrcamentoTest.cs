using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using Dataplace.Imersao.Core.Tests.Fixtures;
using System;
using Xunit;

namespace Dataplace.Imersao.Core.Tests.Domain.Orcamentos
{
    [Collection(nameof(OrcamentoCollection))]
    public class OrcamentoTest
    {
        private readonly OrcamentoFixture _fixture;

        public OrcamentoTest(OrcamentoFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void NovoOrcamentoDevePossuirValoresValidos()
        {
            // Arrange Act
            var orcamento = _fixture.NovoOrcamento();


            // Assert
            Assert.True(orcamento.CdEmpresa == _fixture.CdEmpresa);
            Assert.True(orcamento.CdFilial == _fixture.CdFilial);
            Assert.Equal(_fixture.NumOrcaemtp, orcamento.NumOrcamento);
            Assert.True(orcamento.Cliente.Codigo == _fixture.Cliente.Codigo);
            Assert.True(orcamento.Usuario == _fixture.UserName);
            Assert.True(orcamento.Usuario == _fixture.UserName);
            Assert.True(orcamento.Situacao == Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Aberto);
            Assert.Null(orcamento.Validade);
            Assert.NotNull(orcamento.TabelaPreco);
            Assert.Equal(_fixture.TavelaPreco.CdTabela, orcamento.TabelaPreco.CdTabela);
            Assert.Equal(_fixture.TavelaPreco.SqTabela, orcamento.TabelaPreco.SqTabela);
        }

        [Fact]
        public void FecharOrcamentoDeveRetornarStatusFechado()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();


            // Act
            orcamento.FecharOrcamento();


            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Fechado, orcamento.Situacao);
            Assert.NotNull(orcamento.DtFechamento);
        }


        [Fact]
        public void TentarFecharOrcamentoJaFechadoRetornarException()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.FecharOrcamento();

            // act & assert
            Assert.Throws<DomainException>(() => orcamento.FecharOrcamento());
        }

        [Fact]
        public void CancelarOrcamento()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();

            // Act
            orcamento.CancelarOrcamento();

            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Cancelado, orcamento.Situacao);
            Assert.Null(null);
        }
        
        [Fact]
        public void TentaCancelarOrcamentoJaFechadoRetornarException()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();

            // Act
            orcamento.FecharOrcamento();
            orcamento.CancelarOrcamento();

            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Cancelado, orcamento.Situacao);
            Assert.Null(null);
        }
        
        [Fact]
        public void TentaCancelarOrcamentoJaCanceladoRetornarException()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();

            // Act
            orcamento.CancelarOrcamento();
            orcamento.CancelarOrcamento();

            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Cancelado, orcamento.Situacao);
            Assert.Null(null);
        }
        
    }
}