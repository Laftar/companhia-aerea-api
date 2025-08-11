using Desafio001.Dominio.Entidades.Base;

namespace Desafio001.Dominio.Entidades
{
    public class Passageiro : EntidadeComExclusaoLogica
    {
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Documento { get; private set; } = string.Empty;
        public DateOnly DataNascimento { get; private set; }
        public virtual ICollection<VooPassageiro> VoosPassageiros { get; private set; } = new List<VooPassageiro>();

        protected Passageiro() { }
        public Passageiro(string nome, string documento, DateOnly dataNascimento, string email)
        {
            Nome = nome;
            Documento = documento;
            DataNascimento = dataNascimento;
            Email = email;
        }

        public void AtualizarDados(string? nome, DateOnly? dataNascimento, string? email)
        {
            Nome = nome ?? Nome;
            DataNascimento = dataNascimento ?? DataNascimento;
            Email = email ?? Email;
        }
    }
}
