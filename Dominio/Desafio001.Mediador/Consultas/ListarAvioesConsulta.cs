using Desafio001.Dominio.Contratos;
using MediatR;
namespace Desafio001.Mediador.Consultas
{
    public sealed record ListarAvioesConsulta(ListarAvioesContrato contrato) : IRequest<List<AviaoSalvoContrato>>;
}
