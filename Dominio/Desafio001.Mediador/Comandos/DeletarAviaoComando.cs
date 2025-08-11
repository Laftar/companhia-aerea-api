using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record DeletarAviaoComando(Guid id) : IRequest<Guid>;
}