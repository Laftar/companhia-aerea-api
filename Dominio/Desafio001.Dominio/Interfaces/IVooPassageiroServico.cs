using Desafio001.Dominio.Contratos;

namespace Desafio001.Dominio.Interfaces
{
    public interface IVooPassageiroServico
    {
        Task<List<PassagemSalvaContrato>> ListarPassagensAsync(ListarPassagensContrato contrato, CancellationToken ct);
        Task<PassagemSalvaContrato> ComprarPassagemAsync(ComprarPassagemContrato contrato, CancellationToken ct);
        Task<bool> CheckinPassagemAsync(Guid id, CancellationToken ct);
        Task<Guid> DeletarPassagemAsync(Guid id, CancellationToken ct);
    }
}