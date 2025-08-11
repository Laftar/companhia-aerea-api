using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class DeletarPilotoHandler(IPilotoServico servico) : IRequestHandler<DeletarPilotoComando, Guid>
    {
        private readonly IPilotoServico servico = servico;

        public async Task<Guid> Handle(DeletarPilotoComando cmd, CancellationToken ct)
        {
            return await servico.DeletarPilotoAsync(cmd.id, ct);
        }
    }
}