using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Entidades;
using Desafio001.Servico.Utils;

namespace Desafio001.Servico.Conversores
{
    public class PassageiroConversor : IPassageiroConversor

    {
        public Passageiro ConverterParaEntidade(CriarPassageiroContrato contrato)
        {
            return new Passageiro(
                contrato.Nome,
                StringUtils.SomenteNumeros(contrato.Documento),
                contrato.DataNascimento,
                contrato.Email
            );
        }
    }


}
