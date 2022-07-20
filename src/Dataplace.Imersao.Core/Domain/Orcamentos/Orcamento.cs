using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class Orcamento
    {
        private Orcamento(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoCliente cliente,
            string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
        {
            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            Cliente = cliente;
            NumOrcamento = numOrcamento;
            Usuario = usuario;
            Vendedor = vendedor;
            TabelaPreco = tabelaPreco;

            // default
            Situacao = OrcamentoStatusEnum.Aberto;
            DtOrcamento = DateTime.Now;
            ValotTotal = Decimal.Zero;
            Itens = new List<OrcamentoItem>();
        }

        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public int NumOrcamento { get; private set; }
        public OrcamentoCliente Cliente { get; private set; }
        public DateTime DtOrcamento { get; private set; }
        public decimal ValotTotal { get; private set; }
        public OrcamentoValidade Validade { get; private set; }
        public OrcamentoTabelaPreco TabelaPreco { get; private set; }
        public DateTime? DtFechamento { get; private set; }
        public DateTime? DtCancelamento { get; private set; }
        public OrcamentoVendedor Vendedor { get; private set; }
        public string Usuario { get; private set; }
        public OrcamentoStatusEnum Situacao { get; private set; }
        public ICollection<OrcamentoItem> Itens { get; private set; }


        public void FecharOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Fechado;
            DtFechamento = DateTime.Now.Date;
        }

        public void CancelarOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado!");

            if (Situacao == OrcamentoStatusEnum.Cancelado)
                throw new DomainException("Orçamento já está cancelado!");

            Situacao = OrcamentoStatusEnum.Cancelado;
            DtCancelamento = DateTime.Now;
        }

        public void ReabrirOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Aberto)
                throw new DomainException("Orçamento já está Aberto!");

            if (Situacao == OrcamentoStatusEnum.Cancelado)
                throw new DomainException("Orçamento está cancelado!");

            Situacao = OrcamentoStatusEnum.Aberto;
            DtFechamento = null;
        }

        public void DefinirValidade(int diasValidade)
        {
            this.Validade = new OrcamentoValidade(this, diasValidade);
        }

        public void InserirItens(OrcamentoItem item)
        {
            if (item == null) throw new DomainException("O item é requerido");
            Itens.Add(item);
            ValotTotal += item.Total;
        }

        #region validations

        public List<string> Validations;

        public bool IsValid()
        {
            Validations = new List<string>();

            if (string.IsNullOrEmpty(CdEmpresa))
                Validations.Add("Código da empresa é requirido!");

            if (string.IsNullOrEmpty(CdFilial))
                Validations.Add("Código da filial é requirido!");

            if (ValotTotal <= 0)
                Validations.Add("O valor total do orçamento não pode ser menor ou igual a zero!");

            if (!(DtFechamento is null))
                Validations.Add("O orçamento não pode ser fechado, pois já possui Data de Fechamento!");

            if (Vendedor is null)
                Validations.Add("O vendedor é requerido");

            if (string.IsNullOrEmpty(Usuario))
                Validations.Add("O usuário é requirido");

            if (string.IsNullOrEmpty(TabelaPreco.CdTabela))
                Validations.Add("A tabela de preço é requirido");

            if (Validations.Count > 0)
                return false;
            else
                return true;
        }

        #endregion

        #region factory methods

        public static class Factory
        {
            public static Orcamento Orcamento(string cdEmpresa, string cdFilial, int numOrcamento,
                OrcamentoCliente cliente, string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, cliente, usuario, vendedor, tabelaPreco);
            }

            public static Orcamento OrcamentoRapido(string cdEmpresa, string cdFilial, int numOrcamento, string usuario,
                OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, null, usuario, vendedor, tabelaPreco);
            }
        }

        #endregion
    }
}