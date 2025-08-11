using MediatR;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;

namespace Desafio001.Mediador.Handlers
{

    public class CheckinPassagemHandler : IRequestHandler<CheckinPassagemComando, bool>
    {
        private readonly IVooPassageiroServico servico;

        public CheckinPassagemHandler(IVooPassageiroServico servico)
        {
            this.servico = servico;
        }

        public Task<bool> Handle(CheckinPassagemComando cmd, CancellationToken ct)
        {
            return servico.CheckinPassagemAsync(cmd.Id, ct);
        }
    }


}