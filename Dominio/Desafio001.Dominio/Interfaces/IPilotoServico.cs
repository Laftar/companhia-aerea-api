using Desafio001.Dominio.Contratos;

namespace Desafio001.Dominio.Interfaces
{
    public interface IPilotoServico
    {
        Task<PilotoSalvoContrato> BuscarPilotoAsync( Guid id, CancellationToken ct);
        Task<List<PilotoSalvoContrato>> ListarPilotosAsync(ListarPilotosContrato contrato, CancellationToken ct);
        Task<PilotoSalvoContrato> CriarPilotoAsync(CriarPilotoContrato contrato, CancellationToken ct);
        Task<PilotoSalvoContrato> AtualizarPilotoAsync(Guid id, AtualizarPilotoContrato contrato, CancellationToken ct);
        Task<Guid> DeletarPilotoAsync(Guid id, CancellationToken ct);
    }
}