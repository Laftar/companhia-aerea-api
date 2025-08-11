using Desafio001.Dominio.Entidades;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Servico.Conversores
{
    public class VooPassageiroConversor : IVooPassageiroConversor
    {
        public VooPassageiro ConverterParaEntidade(Guid PassageiroId, Guid VooId)
        {
            return new VooPassageiro(
                PassageiroId,
                VooId);
        }
    }


}
