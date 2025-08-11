using Desafio001.Dominio.Entidades.Enums;

namespace Desafio001.Api.ViewModels
{
    public sealed record PassageiroSalvoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }
        public string Email {  get; set; } = string.Empty;
    }
}
