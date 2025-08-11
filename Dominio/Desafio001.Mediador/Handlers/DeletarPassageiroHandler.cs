using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class DeletarPassageiroHandler(IPassageiroServico servico) : IRequestHandler<DeletarPassageiroComando, Guid>
    {
        private readonly IPassageiroServico servico = servico;

        public async Task<Guid> Handle(DeletarPassageiroComando cmd, CancellationToken ct)
        {
            return await servico.DeletarPassageiroAsync(cmd.id, ct);
        }
    }
}