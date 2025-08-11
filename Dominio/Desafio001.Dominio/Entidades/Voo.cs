using Desafio001.Dominio.Entidades.Base;
using Desafio001.Dominio.Entidades.Enums;

namespace Desafio001.Dominio.Entidades
{
    public class Voo : EntidadeComExclusaoLogica
    {
        public VooStatus Status { get; private set; } = VooStatus.Pendente;

        public Guid AviaoId { get; private set; }
        public virtual Aviao Aviao { get; private set; } = null!;

        public Guid PilotoId { get; private set; }
        public virtual Piloto Piloto { get; private set; } = null!;

        public DateOnly DataVoo { get; private set; }
        public TimeOnly HorarioPrevSaida { get; private set; }
        public TimeOnly HorarioPrevChegada { get; private set; }
        public TimeOnly? HorarioRealSaida { get; private set; }
        public TimeOnly? HorarioRealChegada { get; private set; }

        public int QtdPassagensVendidas { get; private set; }
        public int QtdPassageirosPresentes { get; private set; }

        public virtual ICollection<VooPassageiro> VoosPassageiros { get; private set; } = new List<VooPassageiro>();

        protected Voo() { }

        public Voo(Guid aviaoId, Guid pilotoId, DateOnly dataVoo,
                   TimeOnly horarioPrevSaida, TimeOnly horarioPrevChegada)
        {
            AviaoId = aviaoId;
            PilotoId = pilotoId;
            DataVoo = dataVoo;
            HorarioPrevSaida = horarioPrevSaida;
            HorarioPrevChegada = horarioPrevChegada;
        }

        public void AtualizarVoo(DateOnly dataVoo, TimeOnly horarioPrevSaida, TimeOnly horarioPrevChegada)
        {
            DataVoo = dataVoo;
            HorarioPrevSaida = horarioPrevSaida;
            HorarioPrevChegada = horarioPrevChegada;
        }

        public void ConcluirVoo(TimeOnly horarioRealSaida, TimeOnly horarioRealChegada)
        {
            HorarioRealSaida = horarioRealSaida;
            HorarioRealChegada = horarioRealChegada;
            Status = VooStatus.Concluido;
        }

        public void NovoPassageiro() => QtdPassagensVendidas++;
        public void RetirarPassageiro() => QtdPassagensVendidas--;
        public void CheckinPassageiro() => QtdPassageirosPresentes++;
    }
}
