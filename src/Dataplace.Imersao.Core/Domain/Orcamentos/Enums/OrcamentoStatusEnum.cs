namespace Dataplace.Imersao.Core.Domain.Orcamentos.Enums
{
    public enum OrcamentoStatusEnum
    {
        Aberto,
        Fechado,
        Cancelado
    }

    public static class OrcamentoStatusEnumExtensions
    {
        public static string ToDataValue(this OrcamentoStatusEnum value)
        {
            return
                value == OrcamentoStatusEnum.Aberto ? "P" :
                value == OrcamentoStatusEnum.Fechado ? "F" :
                value == OrcamentoStatusEnum.Cancelado ? "C" : null;
        }

        public static OrcamentoStatusEnum? ToOrcamentoStatusEnum(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return OrcamentoStatusEnum.Aberto;

            OrcamentoStatusEnum? t =
                value == "P" ? OrcamentoStatusEnum.Aberto :
                value == "F" ? OrcamentoStatusEnum.Fechado :
                value == "C" ? OrcamentoStatusEnum.Cancelado : (OrcamentoStatusEnum?)null;

            return t;
        }
    }
}