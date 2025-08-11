using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Contratos;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{

    public class CriarAviaoHandler : IRequestHandler<CriarAviaoComando, AviaoSalvoContrato>
    {
        private readonly IAviaoServico servico;

        public CriarAviaoHandler(IAviaoServico servico)
        {
            this.servico = servico;
        }

        public Task<AviaoSalvoContrato> Handle(CriarAviaoComando cmd, CancellationToken ct)
        {
            return servico.CriarAviaoAsync(cmd.contrato, ct);
        }
    }


}