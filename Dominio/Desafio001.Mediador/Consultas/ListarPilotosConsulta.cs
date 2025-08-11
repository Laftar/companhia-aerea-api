using Desafio001.Dominio.Contratos;
using MediatR;
namespace Desafio001.Mediador.Consultas
{
    public sealed record ListarPilotosConsulta(ListarPilotosContrato contrato) : IRequest<List<PilotoSalvoContrato>>;
}
