using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Consultas;
using MediatR;

namespace Desafio001.Mediador.Handlers
{
    public class ListarAvioesHandler : IRequestHandler<ListarAvioesConsulta, List<AviaoSalvoContrato>>
    {
        private readonly IAviaoServico servico;

        public ListarAvioesHandler(IAviaoServico servico)
        {
            this.servico = servico;
        }

        public async Task<List<AviaoSalvoContrato>> Handle(ListarAvioesConsulta consulta, CancellationToken ct)
        {
            return await servico.ListarAvioesAsync(consulta.contrato, ct);
        }
    }
}
