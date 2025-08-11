namespace Desafio001.Dominio.Interfaces
{
    public interface IMensageriaProdutorServico
    {
        Task PublicarMensagemAsync<T>(T mensagem, CancellationToken ct);
    }
}
