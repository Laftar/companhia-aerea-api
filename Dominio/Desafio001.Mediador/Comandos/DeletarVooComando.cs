using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record DeletarVooComando(Guid id) : IRequest<Guid>;
}