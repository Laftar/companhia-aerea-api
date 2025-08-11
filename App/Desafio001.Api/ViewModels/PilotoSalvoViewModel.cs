namespace Desafio001.Api.ViewModels
{
    public sealed record PilotoSalvoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
        public int QtdVoosRealizados { get; set; }
    }
}
