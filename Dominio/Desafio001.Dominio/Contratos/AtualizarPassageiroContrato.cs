namespace Desafio001.Dominio.Contratos
{
    public sealed record AtualizarPassageiroContrato(
        string? Nome,
        DateOnly? DataNascimento,
        string? Email
    );
}
