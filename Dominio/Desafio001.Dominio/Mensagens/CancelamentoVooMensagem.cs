using Desafio001.Dominio.Utils;

namespace Desafio001.Dominio.Mensagens
{
    [Fila("CancelamentoVoo")]
    public sealed record CancelamentoVooMensagem(
        Guid VooId);
}