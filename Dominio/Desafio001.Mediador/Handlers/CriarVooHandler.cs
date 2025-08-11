using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;
using MediatR;

namespace Desafio001.Mediador.Handlers
{

    public class CriarVooHandler : IRequestHandler<CriarVooComando, VooSalvoContrato>
    {
        private readonly IVooServico servico;

        public CriarVooHandler(IVooServico servico)
        {
            this.servico = servico;
        }

        public Task<VooSalvoContrato> Handle(CriarVooComando cmd, CancellationToken ct)
        {
            return servico.CriarVooAsync(cmd.contrato, ct);
        }
    }


}