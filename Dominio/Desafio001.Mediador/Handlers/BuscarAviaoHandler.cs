using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Contratos;
using Desafio001.Mediador.Consultas;

namespace Desafio001.Mediador.Handlers
{
    public class BuscarAviaoHandler(IAviaoServico servico) : IRequestHandler<BuscarAviaoConsulta, AviaoSalvoContrato>
    {
        private readonly IAviaoServico servico = servico;

        public Task<AviaoSalvoContrato> Handle(BuscarAviaoConsulta consulta, CancellationToken ct)
        {
            return servico.BuscarAviaoAsync(consulta.Id, ct);
        }
    }
}