namespace Desafio001.Dominio.Contratos
{
    public sealed record CriarPilotoContrato(
        string Nome,
        string Documento,
        DateOnly DataNascimento,
        int QtdVoosRealizados);
}
