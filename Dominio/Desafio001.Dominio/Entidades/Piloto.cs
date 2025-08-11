
using Desafio001.Dominio.Entidades.Base;

namespace Desafio001.Dominio.Entidades
{
    public class Piloto : EntidadeComExclusaoLogica
    {
        public string Nome { get; private set; } = string.Empty;
        public DateOnly DataNascimento { get; private set; }
        public int QtdVoosRealizados { get; private set; }
        public string Documento { get; private set; } = string.Empty;
        public virtual ICollection<Voo> Voos { get; private set; } = new List<Voo>();

        public Piloto(
            string nome, 
            string documento, 
            DateOnly dataNascimento, 
            int qtdVoosRealizados)
        {
            Nome = nome;
            Documento = documento;
            DataNascimento = dataNascimento;
            QtdVoosRealizados = qtdVoosRealizados;
        }

        public void AtualizarDados(
            string? nome, 
            DateOnly? dataNascimento, 
            int? qtdVoosRealizados)
        {
            this.Nome = nome ?? this.Nome;
            this.DataNascimento = dataNascimento ?? this.DataNascimento;
            this.QtdVoosRealizados = qtdVoosRealizados ?? this.QtdVoosRealizados;    
        }

        public void VooRealizado()
        {
            this.QtdVoosRealizados++;
        }
    }

}
