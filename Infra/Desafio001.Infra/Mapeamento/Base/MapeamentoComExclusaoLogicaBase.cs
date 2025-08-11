using Desafio001.Dominio.Entidades.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio001.Infra.Mapeamento.Base
{
    public abstract class MapeamentoComExclusaoLogicaBase<TEntidade> : MapeamentoBase<TEntidade>
        where TEntidade : EntidadeComExclusaoLogica
    {
        protected override void Mapear(EntityTypeBuilder<TEntidade> builder)
        {
            builder.Property(e => e.Excluido)
                   .IsRequired()
                   .HasColumnName("EXCLUIDO");

            MapearExclusaoLogica(builder);
        }

        protected abstract void MapearExclusaoLogica(EntityTypeBuilder<TEntidade> builder);
    }
}
