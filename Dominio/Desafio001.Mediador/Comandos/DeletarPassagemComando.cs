using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record DeletarPassagemComando(Guid id) : IRequest<Guid>;
}