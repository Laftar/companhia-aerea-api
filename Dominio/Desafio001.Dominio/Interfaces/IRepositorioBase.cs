
using Desafio001.Dominio.Entidades.Base;

namespace Desafio001.Dominio.Interfaces
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        IQueryable<T> MontarConsulta();

        Task<T?> ObterPorIdAsync(Guid id, CancellationToken ct);

        Task<T> AdicionarAsync(T entidade, CancellationToken ct);

        Task<T> AtualizarAsync(T entidade, CancellationToken ct);
    }

}
