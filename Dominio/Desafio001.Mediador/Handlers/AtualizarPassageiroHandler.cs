using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Contratos;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{
    public class AtualizarPassageiroHandler : IRequestHandler<AtualizarPassageiroComando, PassageiroSalvoContrato>
    {
        private readonly IPassageiroServico servico;
        public AtualizarPassageiroHandler(IPassageiroServico servico)
        {
            this.servico = servico;
        }

        public Task<PassageiroSalvoContrato> Handle(AtualizarPassageiroComando cmd, CancellationToken ct)
        {
            return servico.AtualizarPassageiroAsync(cmd.id, cmd.contrato, ct);
        }
    }

}