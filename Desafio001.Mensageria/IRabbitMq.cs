using RabbitMQ.Client;
namespace Desafio001.Mensageria
{
    public interface IRabbitMq : IAsyncDisposable
    {
        Task<IChannel> BuscarCanalAsync(CancellationToken ct);

        Task PublicarMensagemAsync<T>(string file, T contrato, CancellationToken ct);
    }
}
