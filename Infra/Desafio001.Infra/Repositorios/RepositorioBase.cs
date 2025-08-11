using Desafio001.Dominio.Entidades.Base;
using Desafio001.Dominio.Interfaces;
using Desafio001.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Desafio001.Infra.Repositorios
{
    internal sealed class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {

        private readonly DbSet<T> dbSet;
        private readonly AppDbContext db;

        public RepositorioBase(AppDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public IQueryable<T> MontarConsulta()
        {
            return dbSet.AsQueryable();
        }

        public async Task<T?> ObterPorIdAsync(Guid id, CancellationToken ct)
        {
            return await dbSet.FindAsync(id, ct);
        }

        public async Task<T> AdicionarAsync(T entidade, CancellationToken ct)
        {
            await dbSet.AddAsync(entidade, ct);
            await db.SaveChangesAsync(ct);
            return entidade; 
        }

        public async Task<T> AtualizarAsync(T entidade, CancellationToken ct)
        {
            
            dbSet.Update(entidade);
            await db.SaveChangesAsync(ct);
            return entidade;
        }
    }
}
