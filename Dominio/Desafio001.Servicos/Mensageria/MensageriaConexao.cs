using Desafio001.Dominio.Configuracoes;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Desafio001.Servico.Mensageria
{
    public class MensageriaConexao : IMensageriaConexao
    {
        private readonly ConnectionFactory fabrica;
        private IConnection? conexao;

        public MensageriaConexao(IOptions<MensageriaConfig> configuracao)
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

        public async Task<IChannel> BuscarCanalAsync(string fila, CancellationToken ct)
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
                    queue: fila,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: ct)
                .ConfigureAwait(false);

            return canal;
        }

    }
}
