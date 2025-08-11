namespace Desafio001.Dominio.Contratos
{
    public sealed record CriarPassageiroContrato(
        string Nome,
        string Documento,
        DateOnly DataNascimento,
        string Email
    );
}
