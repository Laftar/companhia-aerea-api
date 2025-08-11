namespace Desafio001.Dominio.Contratos
{
    public sealed record CriarVooContrato(
        Guid AviaoId,
        Guid PilotoId,
        DateOnly DataVoo,
        TimeOnly HorarioPrevSaida,
        TimeOnly HorarioPrevChegada
    );
}
