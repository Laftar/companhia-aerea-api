using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class DeletarVooHandler(IVooServico servico) : IRequestHandler<DeletarVooComando, Guid>
    {
        private readonly IVooServico servico = servico;

        public async Task<Guid> Handle(DeletarVooComando cmd, CancellationToken ct)
        {
            return await servico.DeletarVooAsync(cmd.id, ct);
        }
    }
}