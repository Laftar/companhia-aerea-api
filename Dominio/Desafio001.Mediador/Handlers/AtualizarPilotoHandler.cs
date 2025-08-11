using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Contratos;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class AtualizarPilotoHandler : IRequestHandler<AtualizarPilotoComando, PilotoSalvoContrato>
    {
        private readonly IPilotoServico servico;
        public AtualizarPilotoHandler(IPilotoServico servico)
        {
            this.servico = servico;
        }

        public Task<PilotoSalvoContrato> Handle(AtualizarPilotoComando cmd, CancellationToken ct)
        {
            return servico.AtualizarPilotoAsync(cmd.id, cmd.contrato, ct);
        }
    }

}