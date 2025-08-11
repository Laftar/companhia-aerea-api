using RabbitMQ.Client;

namespace Desafio001.Servico.Mensageria
{
    public interface IMensageriaConexao : IAsyncDisposable
    {
        Task<IChannel> BuscarCanalAsync(string fila, CancellationToken ct);
    }
}
