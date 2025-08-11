namespace Desafio001.Dominio.Contratos
{
    public sealed record CriarAviaoContrato(
        string Marca,
        string Modelo,
        int AnoFabricacao,
        int QtdMaxPassageiros,
        int QtdVoosRealizados);
}
