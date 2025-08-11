using Desafio001.Dominio.Contratos;

namespace Desafio001.Dominio.Interfaces
{
    public interface IMensageriaConsumidorServico
    {
        Task NotificarOverbookingVooAsync(NotificarOverbookingVooContrato contrato, CancellationToken ct);

        Task NotificarCancelamentoVooAsync(NotificarCancelamentoVooContrato contrato, CancellationToken ct);
    }
}