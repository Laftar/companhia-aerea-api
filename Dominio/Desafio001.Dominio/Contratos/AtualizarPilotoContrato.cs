namespace Desafio001.Dominio.Contratos
{
    public sealed record AtualizarPilotoContrato( 
        string? Nome,
        DateOnly? DataNascimento,
        int? QtdVoosRealizados);

}
