using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Consultas;
using MediatR;

namespace Desafio001.Mediador.Handlers
{
    public class ListarPassagensHandler : IRequestHandler<ListarPassagensConsulta, List<PassagemSalvaContrato>>
    {
        private readonly IVooPassageiroServico servico;

        public ListarPassagensHandler(IVooPassageiroServico servico)
        {
            this.servico = servico;
        }

        public async Task<List<PassagemSalvaContrato>> Handle(ListarPassagensConsulta consulta, CancellationToken ct)
        {
            return await servico.ListarPassagensAsync(consulta.contrato, ct);
        }
    }
}
