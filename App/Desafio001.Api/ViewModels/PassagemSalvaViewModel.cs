using Desafio001.Dominio.Entidades.Enums;

namespace Desafio001.Api.ViewModels
{
    public sealed record PassagemSalvaViewModel
    {
        public Guid Id { get; set; }
        public VooPassageiroStatus PassagemStatus { get; set; }

        public Guid PassageiroId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DateOnly DataNascimento { get; set; }

        public Guid VooId { get; set; }
        public DateOnly DataVoo { get; set; }
        public VooStatus VooStatus { get; set; }

        
    }
}
