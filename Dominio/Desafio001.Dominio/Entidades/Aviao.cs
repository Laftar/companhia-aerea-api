


using Desafio001.Dominio.Entidades.Base;

namespace Desafio001.Dominio.Entidades
{
    public class Aviao : EntidadeComExclusaoLogica
    {
        public int AnoFabricacao { get; private set; }
        public string Marca { get; private set; } = string.Empty;
        public string Modelo { get; private set; } = string.Empty;
        public int QtdMaxPassageiros { get; private set; }
        public int QtdVoosRealizados { get; private set; }
        public virtual ICollection<Voo> Voos { get; private set; } = new List<Voo>();

        public Aviao(
            string marca,
            string modelo,
            int anoFabricacao, 
            int qtdMaxPassageiros,
            int qtdVoosRealizados)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.AnoFabricacao = anoFabricacao;
            this.QtdMaxPassageiros = qtdMaxPassageiros;
            this.QtdVoosRealizados = qtdVoosRealizados;
        }

        public void AtualizarDados(
            string? modelo,
            int? anoFabricacao)
        {
            this.Modelo= modelo ?? this.Modelo;
            this.AnoFabricacao = anoFabricacao ?? this.AnoFabricacao;
        }

        public void VooRealizado()
        {
            this.QtdVoosRealizados++;
        }
    }
}
