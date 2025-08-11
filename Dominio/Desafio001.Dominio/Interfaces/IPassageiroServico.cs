using Desafio001.Dominio.Contratos;

namespace Desafio001.Dominio.Interfaces
{
    public interface IPassageiroServico
    {
        Task<PassageiroSalvoContrato> BuscarPassageiroAsync(Guid id, CancellationToken ct);
        Task<List<PassageiroSalvoContrato>> ListarPassageirosAsync(ListarPassageirosContrato contrato, CancellationToken ct);
        Task<PassageiroSalvoContrato> CriarPassageiroAsync(CriarPassageiroContrato contrato, CancellationToken ct);
        Task<PassageiroSalvoContrato> AtualizarPassageiroAsync(Guid id, AtualizarPassageiroContrato contrato, CancellationToken ct);
        Task<Guid> DeletarPassageiroAsync(Guid id, CancellationToken ct);
    }
}