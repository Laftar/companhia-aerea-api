using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Contratos;
using Desafio001.Mediador.Consultas;

namespace Desafio001.Mediador.Handlers
{
    public class BuscarPassageiroHandler(IPassageiroServico servico) : IRequestHandler<BuscarPassageiroConsulta, PassageiroSalvoContrato>
    {
        private readonly IPassageiroServico servico = servico;

        public Task<PassageiroSalvoContrato> Handle(BuscarPassageiroConsulta consulta, CancellationToken ct)
        {
            return servico.BuscarPassageiroAsync(consulta.Id, ct);
        }
    }
}