using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class DeletarAviaoHandler(IAviaoServico servico) : IRequestHandler<DeletarAviaoComando, Guid>
    {
        private readonly IAviaoServico servico = servico;

        public async Task<Guid> Handle(DeletarAviaoComando cmd, CancellationToken ct)
        {
            return await servico.DeletarAviaoAsync(cmd.id, ct);
        }
    }
}