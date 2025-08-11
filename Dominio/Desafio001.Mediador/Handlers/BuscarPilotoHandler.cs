using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Contratos;
using Desafio001.Mediador.Consultas;

namespace Desafio001.Mediador.Handlers
{
    public class BuscarPilotoHandler(IPilotoServico servico) : IRequestHandler<BuscarPilotoConsulta, PilotoSalvoContrato>
    {
        private readonly IPilotoServico servico = servico;

        public Task<PilotoSalvoContrato> Handle(BuscarPilotoConsulta consulta, CancellationToken ct)
        {
            return servico.BuscarPilotoAsync(consulta.Id, ct);
        }
    }
}