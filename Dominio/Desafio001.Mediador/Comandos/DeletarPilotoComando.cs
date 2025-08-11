using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record DeletarPilotoComando(Guid id) : IRequest<Guid>;
}