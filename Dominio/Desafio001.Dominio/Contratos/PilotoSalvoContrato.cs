namespace Desafio001.Dominio.Contratos
{
    public sealed record PilotoSalvoContrato(
        Guid Id,
        string Nome,
        string Documento,
        DateOnly DataNascimento,
        int QtdVoosRealizados
        );
}
