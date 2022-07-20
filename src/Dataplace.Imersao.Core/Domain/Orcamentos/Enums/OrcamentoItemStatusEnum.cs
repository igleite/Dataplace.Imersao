namespace Dataplace.Imersao.Core.Domain.Orcamentos.Enums
{
    public enum OrcamentoItemStatusEnum
    {
        Aberto,
        Fechado,
        Cancelado
    }

    public static class OrcamentoItemStatusEnumExtensions
    {
        public static string ToDataValue(this OrcamentoItemStatusEnum value)
        {
            return
                value == OrcamentoItemStatusEnum.Aberto ? "P" :
                value == OrcamentoItemStatusEnum.Fechado ? "F" :
                value == OrcamentoItemStatusEnum.Cancelado ? "C" : null;
        }

        public static OrcamentoItemStatusEnum? ToOrcamentoItemStatusEnum(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return OrcamentoItemStatusEnum.Aberto;

            OrcamentoItemStatusEnum? t =
                value == "P" ? OrcamentoItemStatusEnum.Aberto :
                value == "F" ? OrcamentoItemStatusEnum.Fechado :
                value == "C" ? OrcamentoItemStatusEnum.Cancelado : (OrcamentoItemStatusEnum?)null;

            return t;
        }
    }
}