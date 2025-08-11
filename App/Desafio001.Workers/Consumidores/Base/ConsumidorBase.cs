using Desafio001.Dominio.Utils;
using Desafio001.Servico.Mensageria;
using MediatR;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;
using System.Text.Json;

internal abstract class ConsumidorBase<TMensagem> : BackgroundService where TMensagem : class
{
    private readonly IMensageriaConexao conexao;
    protected readonly IMediator mediator;
    private IChannel? canal;
    private string nomeFila = "";

    protected ConsumidorBase(IMensageriaConexao conexao, IMediator mediator)
    {
        this.conexao = conexao;
        this.mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var fila = typeof(TMensagem).GetCustomAttribute<Fila>()
                  ?? throw new InvalidOperationException($"Classe {typeof(TMensagem).Name} não possui [Fila].");

        nomeFila = fila.Nome;

        canal = await conexao.BuscarCanalAsync(nomeFila, ct).ConfigureAwait(false);

        var consumidor = new AsyncEventingBasicConsumer(canal);

        consumidor.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var mensagem = JsonSerializer.Deserialize<TMensagem>(json);

                if (mensagem is not null)
                {
                    await ConsumirMensagem(mensagem, ct);
                    await canal.BasicAckAsync(ea.DeliveryTag, false, ct);
                    Console.WriteLine("Mensagem processada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Mensagem inválida.");
                    await canal.BasicNackAsync(ea.DeliveryTag, false, requeue: false, ct);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                await canal.BasicNackAsync(ea.DeliveryTag, false, requeue: false, ct);
            }
        };

        await canal.BasicConsumeAsync(
            queue: nomeFila,
            autoAck: false,
            consumer: consumidor,
            cancellationToken: ct
        );

        await Task.Delay(Timeout.Infinite, ct);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (canal is not null)
        {
            Console.WriteLine($"Encerrando consumidor da fila '{nomeFila}'...");
            await canal.CloseAsync();
            canal.Dispose();
        }

        await base.StopAsync(cancellationToken);
    }

    public abstract Task ConsumirMensagem(TMensagem mensagem, CancellationToken ct);
}
