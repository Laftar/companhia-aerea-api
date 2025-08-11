using Desafio001.Dominio.Entidades;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Servico.Conversores
{
    public interface IPilotoConversor
    {
        Piloto ConverterParaEntidade(CriarPilotoContrato contrato);
    }
}