using Desafio001.Dominio.Contratos;

namespace Desafio001.Dominio.Interfaces
{
    public interface IAviaoServico
    {
        Task<AviaoSalvoContrato> BuscarAviaoAsync( Guid id, CancellationToken ct);
        Task<List<AviaoSalvoContrato>> ListarAvioesAsync( ListarAvioesContrato contrato, CancellationToken ct);
        Task<AviaoSalvoContrato> CriarAviaoAsync(CriarAviaoContrato contrato, CancellationToken ct);
        Task<AviaoSalvoContrato> AtualizarAviaoAsync(Guid id, AtualizarAviaoContrato contrato, CancellationToken ct);
        Task<Guid> DeletarAviaoAsync(Guid id, CancellationToken ct);
    }
}