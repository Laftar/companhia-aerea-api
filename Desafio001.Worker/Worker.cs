using Desafio001.Dominio.Contratos;
using Desafio001.Mensageria.Comandos;
using Desafio001.Servico.Mensageria;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class Worker : BackgroundService
{
    private readonly IMensageriaConexao mensageria;
    private readonly IMediator mediator;

    public Worker(IMensageriaConexao mensageria, IMediator mediator)
    {
        this.mensageria = mensageria;
        this.mediator = mediator;
    }


    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await using var canal = await mensageria.BuscarCanalAsync(ct);


        var overbooking = new AsyncEventingBasicConsumer(canal);
        overbooking.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var contrato = JsonSerializer.Deserialize<NotificarOverbookingVooContrato>(json);

                if (contrato != null)
                {

                    await mediator.Send(new NotificarOverbookingVooComando(contrato), ct);
                    Console.WriteLine($"[OVERBOOKING] Processado para VooId {contrato.VooId}");
                    await canal.BasicAckAsync(ea.DeliveryTag, false, ct);
                }
                else
                {
                    Console.WriteLine("[OVERBOOKING] Mensagem inválida.");
                    await canal.BasicNackAsync(ea.DeliveryTag, false, requeue : false, ct);
                }

            } catch
            {

            }
        };
        await canal.BasicConsumeAsync(
            queue: "notificar_overbooking",
            autoAck: false,
            consumer: overbooking,
            cancellationToken: ct
        );


        var cancelamento = new AsyncEventingBasicConsumer(canal);
        cancelamento.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var contrato = JsonSerializer.Deserialize<NotificarCancelamentoVooContrato>(json);

                if (contrato != null)
                {
                    await mediator.Send(new NotificarCancelamentoVooComando(contrato), ct);
                    Console.WriteLine($"[CANCELAMENTO] Processado para VooId {contrato.VooId}");
                    await canal.BasicAckAsync(ea.DeliveryTag, false, ct);
                }
                else
                {
                    Console.WriteLine("[CANCELAMENTO] Mensagem inválida.");
                    await canal.BasicNackAsync(ea.DeliveryTag, false, false, ct);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CANCELAMENTO] Erro: {ex.Message}");
            }
        };

        await canal.BasicConsumeAsync(
            queue: "notificar_cancelamentovoo",
            autoAck: false,
            consumer: cancelamento,
            cancellationToken: ct
        );


        await Task.Delay(Timeout.Infinite, ct);
    }
}
