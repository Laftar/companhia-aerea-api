namespace Desafio001.Dominio.Contratos
{
    public sealed record ListarVoosContrato (
        DateOnly? DataVoo,
        Guid? AviaoId,
        Guid? PilotoId);
}
