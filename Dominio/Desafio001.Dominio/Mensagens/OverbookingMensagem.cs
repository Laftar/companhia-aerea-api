using Desafio001.Dominio.Utils;

namespace Desafio001.Dominio.Mensagens
{
    [Fila("Overbooking")]
    public sealed record OverbookingMensagem(
        Guid VooId);
}