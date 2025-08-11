namespace Desafio001.Dominio.Contratos
{
    public sealed record AviaoSalvoContrato(
        Guid Id,
        string Marca,
        string Modelo,
        int AnoFabricacao,
        int QtdMaxPassageiros,
        int QtdVoosRealizados);
}
