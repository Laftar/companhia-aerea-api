namespace Desafio001.Api.ViewModels
{
    public sealed record CriarPilotoViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public string Documento {  get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
        public int QtdVoosRealizados { get; set; }
    }
}
