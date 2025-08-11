using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class NotificarOverbookingVooHandler(IMensageriaConsumidorServico servico) : IRequestHandler<NotificarOverbookingVooComando>
    {
        private readonly IMensageriaConsumidorServico servico = servico;

        public async Task Handle(NotificarOverbookingVooComando cmd, CancellationToken ct)
        {
            await servico.NotificarOverbookingVooAsync(cmd.contrato, ct);
        }
    }
}
