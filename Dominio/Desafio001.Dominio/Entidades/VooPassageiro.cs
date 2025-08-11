using Desafio001.Dominio.Entidades.Base;
using Desafio001.Dominio.Entidades.Enums;

namespace Desafio001.Dominio.Entidades
{
    public class VooPassageiro : EntidadeComExclusaoLogica
    {
        public VooPassageiroStatus Status { get; private set; } = VooPassageiroStatus.Pendente;

        public Guid PassageiroId { get; private set; }
        public virtual Passageiro Passageiro { get; private set; } = null!;

        public Guid VooId { get; private set; }
        public virtual Voo Voo { get; private set; } = null!;

        protected VooPassageiro() { }
        public VooPassageiro(Guid passageiroId, Guid vooId)
        {
            PassageiroId = passageiroId;
            VooId = vooId;
            Status = VooPassageiroStatus.Pendente;
        }

        public void AlterarStatus(VooPassageiroStatus status) =>
            Status = status;
    }
}
