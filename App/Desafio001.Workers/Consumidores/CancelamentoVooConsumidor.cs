using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Mensagens;
using Desafio001.Mediador.Comandos;
using Desafio001.Servico.Mensageria;
using MediatR;

namespace Desafio001.Workers.Consumidores
{
    internal sealed class CancelamentoVooConsumidor : ConsumidorBase<CancelamentoVooMensagem>
    {
        public CancelamentoVooConsumidor(IMensageriaConexao conexao, IMediator mediator)
            : base(conexao, mediator)
        {}

        public override async Task ConsumirMensagem(CancelamentoVooMensagem mensagem, CancellationToken ct)
        {
            var contrato = new NotificarCancelamentoVooContrato(VooId: mensagem.VooId);
            var comando = new NotificarCancelamentoVooComando(contrato);
            await mediator.Send(comando, ct);
            Console.WriteLine($"[Cancelamento] Processado para VooId {contrato.VooId}");
        }
    }
}
