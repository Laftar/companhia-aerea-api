using Desafio001.Dominio.Contratos;

namespace Desafio001.Dominio.Interfaces
{
    public interface IVooServico
    {
        Task<VooSalvoContrato> BuscarVooAsync(Guid id, CancellationToken ct);
        Task<List<VooSalvoContrato>> ListarVoosAsync(ListarVoosContrato contrato, CancellationToken ct);
        Task<VooSalvoContrato> CriarVooAsync(CriarVooContrato contrato, CancellationToken ct);
        Task<VooSalvoContrato> AtualizarVooAsync(Guid id, AtualizarVooContrato contrato, CancellationToken ct);
        Task<VooSalvoContrato> ConcluirVooAsync(Guid id, ConcluirVooContrato contrato, CancellationToken ct);
        Task<Guid> DeletarVooAsync(Guid id, CancellationToken ct);
    }
}