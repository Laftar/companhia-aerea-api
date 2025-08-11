using Desafio001.Dominio.Entidades;
using Desafio001.Infra.Mapeamento.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio001.Infra.Mapeamento
{
    public class AviaoMapeamento : MapeamentoComExclusaoLogicaBase<Aviao>
    {
        protected override void MapearExclusaoLogica(EntityTypeBuilder<Aviao> builder)
        {
            builder.ToTable("AVIOES");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()")
                   .HasColumnName("ID");

            builder.Property(a => a.AnoFabricacao)
                   .IsRequired()
                   .HasColumnName("ANO_FABRICACAO");

            builder.Property(a => a.Marca)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("MARCA");

            builder.Property(a => a.Modelo)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("MODELO");

            builder.Property(a => a.QtdMaxPassageiros)
                   .IsRequired()
                   .HasColumnName("QTD_MAX_PASSAGEIROS");

            builder.Property(a => a.QtdVoosRealizados)
                   .IsRequired()
                   .HasColumnName("QTD_VOOS_REALIZADOS");
        }
    }
}
