using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Entidades;

namespace Desafio001.Servico.Conversores
{
    public interface IVooPassageiroConversor
    {
        VooPassageiro ConverterParaEntidade(Guid PassageiroId, Guid VooId);

    }
}