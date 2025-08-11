using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Interfaces;
using Desafio001.Mediador.Comandos;
using MediatR;

namespace Desafio001.Mediador.Handlers
{

    public class CriarPilotoHandler : IRequestHandler<CriarPilotoComando, PilotoSalvoContrato>
    {
        private readonly IPilotoServico servico;

        public CriarPilotoHandler(IPilotoServico servico)
        {
            this.servico = servico;
        }

        public Task<PilotoSalvoContrato> Handle(CriarPilotoComando cmd, CancellationToken ct)
        {
            return servico.CriarPilotoAsync(cmd.contrato, ct);
        }
    }


}