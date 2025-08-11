namespace Desafio001.Dominio.Contratos
{
    public sealed record PassageiroSalvoContrato(
        Guid Id,
        string Nome,
        string Documento,
        DateOnly DataNascimento,
        string Email
    );
}
