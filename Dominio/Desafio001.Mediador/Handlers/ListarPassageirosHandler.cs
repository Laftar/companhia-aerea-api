using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Consultas;
using MediatR;

namespace Desafio001.Mediador.Handlers
{
    public class ListarPassageirosHandler : IRequestHandler<ListarPassageirosConsulta, List<PassageiroSalvoContrato>>
    {
        private readonly IPassageiroServico servico;

        public ListarPassageirosHandler(IPassageiroServico servico)
        {
            this.servico = servico;
        }

        public async Task<List<PassageiroSalvoContrato>> Handle(ListarPassageirosConsulta consulta, CancellationToken ct)
        {
            return await servico.ListarPassageirosAsync(consulta.contrato, ct);
        }
    }
}
