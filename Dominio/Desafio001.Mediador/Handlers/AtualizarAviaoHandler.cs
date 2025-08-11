using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Contratos;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class AtualizarAviaoHandler : IRequestHandler<AtualizarAviaoComando, AviaoSalvoContrato>
    {
        private readonly IAviaoServico servico;
        public AtualizarAviaoHandler(IAviaoServico servico)
        {
            this.servico = servico;
        }

        public Task<AviaoSalvoContrato> Handle(AtualizarAviaoComando cmd, CancellationToken ct)
        {
            return servico.AtualizarAviaoAsync(cmd.id, cmd.contrato, ct);
        }
    }

}