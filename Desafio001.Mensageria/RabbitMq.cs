using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Desafio001.Dominio.Configuracoes;

namespace Desafio001.Mensageria
{
    public class RabbitMq : IRabbitMq
    {
        private readonly ConnectionFactory fabrica;
        private IConnection? conexao;

        public RabbitMq(IOptions<MensageriaConfig> configuracao)
        {
            var config = configuracao.Value;
            this.fabrica = new ConnectionFactory
            {
                HostName = config.HostName,
                UserName = config.UserName,
                Password = config.Password,
                Port = config.Port,
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true
            };
        }

        public async ValueTask DisposeAsync()
        {
            if (conexao is not null) await conexao.CloseAsync().ConfigureAwait(false);
        }

        public async Task<IChannel> BuscarCanalAsync(CancellationToken ct)
        {
            if (conexao is null || !conexao.IsOpen)
            {
                conexao = await fabrica
                    .CreateConnectionAsync(ct)
                    .ConfigureAwait(false);
            }

            var canal = await conexao
                .CreateChannelAsync(null, ct)
                .ConfigureAwait(false);

            await canal.QueueDeclareAsync(
                    queue: "notificar_overbooking",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: ct)
                .ConfigureAwait(false);

            await canal.QueueDeclareAsync(
                    queue: "notificar_cancelamentovoo",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: ct)
                .ConfigureAwait(false);

            return canal;
        }

        public async Task PublicarMensagemAsync<T>(string fila, T contrato, CancellationToken ct)
        {
            var canal = await BuscarCanalAsync(ct).ConfigureAwait(false);
            var json = JsonSerializer.Serialize(contrato);
            var body = Encoding.UTF8.GetBytes(json);

            await canal.BasicPublishAsync(
                    exchange: string.Empty,
                    routingKey: fila,
                    mandatory: false,
                    body: body,
                    cancellationToken: ct)
                .ConfigureAwait(false);
        }
    }
}
