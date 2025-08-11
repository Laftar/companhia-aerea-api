using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio001.Infra.Mapeamento.Base
{
    public abstract class MapeamentoBase<TEntidade> : IEntityTypeConfiguration<TEntidade>
        where TEntidade : class
    {
        public void Configure(EntityTypeBuilder<TEntidade> builder)
        {

            builder.ToTable(typeof(TEntidade).Name);
            Mapear(builder);
        }

        protected abstract void Mapear(EntityTypeBuilder<TEntidade> builder);
    }
}
