using Desafio001.Dominio.Contratos;
using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record AtualizarAviaoComando(Guid id, AtualizarAviaoContrato contrato) : IRequest<AviaoSalvoContrato>;
}