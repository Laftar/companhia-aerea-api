using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;
using MediatR;

namespace Desafio001.Mediador.Handlers
{

    public class ConcluirVooHandler : IRequestHandler<ConcluirVooComando, VooSalvoContrato>
    {
        private readonly IVooServico servico;

        public ConcluirVooHandler(IVooServico servico)
        {
            this.servico = servico;
        }

        public Task<VooSalvoContrato> Handle(ConcluirVooComando cmd, CancellationToken ct)
        {
            return servico.ConcluirVooAsync(cmd.id, cmd.contrato, ct);
        }
    }


}