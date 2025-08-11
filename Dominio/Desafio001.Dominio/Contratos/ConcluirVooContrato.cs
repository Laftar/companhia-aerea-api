namespace Desafio001.Dominio.Contratos
{
    public sealed record ConcluirVooContrato(
        TimeOnly HorarioRealSaida,
        TimeOnly HorarioRealChegada
    );
}
