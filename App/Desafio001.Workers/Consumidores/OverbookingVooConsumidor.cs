using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Mensagens;
using Desafio001.Mediador.Comandos;
using Desafio001.Servico.Mensageria;
using MediatR;

namespace Desafio001.Workers.Consumidores
{
    internal sealed class OverbookingVooConsumidor : ConsumidorBase<OverbookingMensagem>
    {
        public OverbookingVooConsumidor(IMensageriaConexao conexao, IMediator mediator)
            : base(conexao, mediator)
        {}

        public override async Task ConsumirMensagem(OverbookingMensagem mensagem, CancellationToken ct)
        {
            var contrato = new NotificarOverbookingVooContrato(VooId: mensagem.VooId);
            var comando = new NotificarOverbookingVooComando(contrato);
            await mediator.Send(comando, ct);
            Console.WriteLine($"[OVERBOOKING] Processado para VooId {contrato.VooId}");
        }
    }
}
