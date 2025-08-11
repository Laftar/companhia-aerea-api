namespace Desafio001.Api.ViewModels
{
    public sealed record CriarPassageiroViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public string Documento {  get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
        public string Email { get; set; } = string.Empty ;
    }
}
