using Desafio001.Dominio.Contratos;
using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public sealed record CriarAviaoComando(CriarAviaoContrato contrato) : IRequest<AviaoSalvoContrato>;
}
