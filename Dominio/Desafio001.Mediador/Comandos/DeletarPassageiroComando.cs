using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record DeletarPassageiroComando(Guid id) : IRequest<Guid>;
}