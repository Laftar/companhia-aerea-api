using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class DeletarPassagemHandler(IVooPassageiroServico servico) : IRequestHandler<DeletarPassagemComando, Guid>
    {
        private readonly IVooPassageiroServico servico = servico;

        public async Task<Guid> Handle(DeletarPassagemComando cmd, CancellationToken ct)
        {
            return await servico.DeletarPassagemAsync(cmd.id, ct);
        }
    }
}