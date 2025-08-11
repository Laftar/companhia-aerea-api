using Desafio001.Dominio.Entidades;
using Desafio001.Dominio.Contratos;
using Desafio001.Servico.Utils;

namespace Desafio001.Servico.Conversores
{
    public class PilotoConversor : IPilotoConversor
    {
        public Piloto ConverterParaEntidade(CriarPilotoContrato contrato)
        {

            return new Piloto(
                    nome: contrato.Nome,
                    documento : StringUtils.SomenteNumeros(contrato.Documento),
                    contrato.DataNascimento,
                    contrato.QtdVoosRealizados);
        }
    }


}
