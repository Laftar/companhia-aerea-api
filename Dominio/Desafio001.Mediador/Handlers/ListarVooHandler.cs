using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Consultas;
using MediatR;

namespace Desafio001.Mediador.Handlers
{
    public class ListarVoosHandler : IRequestHandler<ListarVoosConsulta, List<VooSalvoContrato>>
    {
        private readonly IVooServico servico;

        public ListarVoosHandler(IVooServico servico)
        {
            this.servico = servico;
        }

        public async Task<List<VooSalvoContrato>> Handle(ListarVoosConsulta consulta, CancellationToken ct)
        {
            return await servico.ListarVoosAsync(consulta.contrato, ct);
        }
    }
}
