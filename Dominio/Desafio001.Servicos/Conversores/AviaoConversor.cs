using Desafio001.Dominio.Entidades;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Servico.Conversores
{
    public class AviaoConversor : IAviaoConversor
    {
        public Aviao ConverterParaEntidade(CriarAviaoContrato contrato)
        {
            return new Aviao(
                    contrato.Marca,
                    contrato.Modelo,
                    contrato.AnoFabricacao,
                    contrato.QtdMaxPassageiros,
                    contrato.QtdVoosRealizados
                );
        }
    }


}
