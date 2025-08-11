using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Consultas;
using MediatR;

namespace Desafio001.Mediador.Handlers
{
    public class ListarPilotosHandler : IRequestHandler<ListarPilotosConsulta, List<PilotoSalvoContrato>>
    {
        private readonly IPilotoServico servico;

        public ListarPilotosHandler(IPilotoServico servico)
        {
            this.servico = servico;
        }

        public async Task<List<PilotoSalvoContrato>> Handle(ListarPilotosConsulta consulta, CancellationToken ct)
        {
            return await servico.ListarPilotosAsync(consulta.contrato, ct);
        }
    }
}
