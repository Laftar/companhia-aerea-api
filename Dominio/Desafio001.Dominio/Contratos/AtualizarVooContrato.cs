namespace Desafio001.Dominio.Contratos
{
    public sealed record AtualizarVooContrato(
        DateOnly DataVoo,
        TimeOnly HorarioPrevSaida,
        TimeOnly HorarioPrevChegada);

}
