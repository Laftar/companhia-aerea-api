using Desafio001.Dominio.Contratos;
using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record AtualizarPilotoComando(Guid id, AtualizarPilotoContrato contrato) : IRequest<PilotoSalvoContrato>;
}