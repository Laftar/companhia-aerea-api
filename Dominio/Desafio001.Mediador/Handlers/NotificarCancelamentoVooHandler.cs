using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;
using MediatR;

namespace Desafio001.Mediador.Handlers
{
    public class NotificarCancelamentoVooHandler(IMensageriaConsumidorServico servico) : IRequestHandler<NotificarCancelamentoVooComando>
    {
        private readonly IMensageriaConsumidorServico servico = servico;

        public async Task Handle(NotificarCancelamentoVooComando cmd, CancellationToken ct)
        {
            await servico.NotificarCancelamentoVooAsync(cmd.contrato, ct);
        }
    }
}
