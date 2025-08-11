using Desafio001.Dominio.Entidades;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Servico.Conversores
{
    public class VooConversor : IVooConversor
    {
        public Voo ConverterParaEntidade(CriarVooContrato contrato)
        {

            return new Voo(
                contrato.AviaoId,
                contrato.PilotoId,
                contrato.DataVoo,
                contrato.HorarioPrevSaida,
                contrato.HorarioPrevChegada
            );
        }
    }


}
