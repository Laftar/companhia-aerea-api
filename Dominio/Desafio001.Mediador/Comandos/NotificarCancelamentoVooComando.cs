using Desafio001.Dominio.Contratos;
using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public record NotificarCancelamentoVooComando(NotificarCancelamentoVooContrato contrato) : IRequest;
}
