using Desafio001.Dominio.Contratos;
using MediatR;

namespace Desafio001.Mediador.Comandos
{
    public record NotificarOverbookingVooComando(NotificarOverbookingVooContrato contrato) : IRequest;
}
