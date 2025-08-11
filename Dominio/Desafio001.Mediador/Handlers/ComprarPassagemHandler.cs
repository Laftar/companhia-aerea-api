using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Contratos;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{

    public class ComprarPassagemHandler : IRequestHandler<ComprarPassagemComando, PassagemSalvaContrato>
    {
        private readonly IVooPassageiroServico servico;

        public ComprarPassagemHandler(IVooPassageiroServico servico)
        {
            this.servico = servico;
        }

        public Task<PassagemSalvaContrato> Handle(ComprarPassagemComando cmd, CancellationToken ct)
        {
            return servico.ComprarPassagemAsync(cmd.contrato, ct);
        }
    }


}