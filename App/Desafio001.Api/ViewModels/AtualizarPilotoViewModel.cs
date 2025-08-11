namespace Desafio001.Api.ViewModels
{
    public sealed record AtualizarPilotoViewModel
    {
        public string? Nome { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public int? QtdVoosRealizados { get; set; }
    }
}
