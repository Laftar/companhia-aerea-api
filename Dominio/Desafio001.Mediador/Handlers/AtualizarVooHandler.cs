using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;
using MediatR;

namespace Desafio001.Mediador.Handlers
{

    public class AtualizarVooHandler : IRequestHandler<AtualizarVooComando, VooSalvoContrato>
    {
        private readonly IVooServico servico;

        public AtualizarVooHandler(IVooServico servico)
        {
            this.servico = servico;
        }

        public Task<VooSalvoContrato> Handle(AtualizarVooComando cmd, CancellationToken ct)
        {
            return servico.AtualizarVooAsync(cmd.id, cmd.contrato, ct);
        }
    }


}