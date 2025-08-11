using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;
using MediatR;

namespace Desafio001.Mediador.Handlers
{

    public class CriarPassageiroHandler : IRequestHandler<CriarPassageiroComando, PassageiroSalvoContrato>
    {
        private readonly IPassageiroServico servico;

        public CriarPassageiroHandler(IPassageiroServico servico)
        {
            this.servico = servico;
        }

        public Task<PassageiroSalvoContrato> Handle(CriarPassageiroComando cmd, CancellationToken ct)
        {
            return servico.CriarPassageiroAsync(cmd.contrato, ct);
        }
    }


}