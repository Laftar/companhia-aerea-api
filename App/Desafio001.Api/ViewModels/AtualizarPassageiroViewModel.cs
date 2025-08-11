namespace Desafio001.Api.ViewModels
{
    public sealed record AtualizarPassageiroViewModel
    {
        public string? Nome { get; set; } = string.Empty;
        public DateOnly? DataNascimento { get; set; }

        public string? Email { get; set; } = string.Empty;
    }
}
