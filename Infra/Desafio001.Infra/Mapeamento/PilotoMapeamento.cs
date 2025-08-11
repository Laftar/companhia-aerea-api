using Desafio001.Dominio.Entidades;
using Desafio001.Infra.Mapeamento.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio001.Infra.Mapeamento
{
    public class PilotoMapeamento : MapeamentoComExclusaoLogicaBase<Piloto>
    {
        protected override void MapearExclusaoLogica(EntityTypeBuilder<Piloto> builder)
        {
            builder.ToTable("PILOTOS");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()")
                   .HasColumnName("ID");

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasMaxLength(150)
                   .HasColumnName("NOME");

            builder.Property(p => p.Documento)
                   .IsRequired()
                   .HasMaxLength(11)
                   .HasColumnName("DOCUMENTO");

            builder.Property(p => p.DataNascimento)
                   .IsRequired()
                   .HasColumnName("DATA_NASCIMENTO");

            builder.Property(p => p.QtdVoosRealizados)
                   .IsRequired()
                   .HasColumnName("QTD_VOOS_REALIZADOS");
        }
    }
}
