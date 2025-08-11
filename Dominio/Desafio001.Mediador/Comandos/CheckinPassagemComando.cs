using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record CheckinPassagemComando(Guid Id) : IRequest<bool>;
}
