using Desafio001.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio001.Infra.Mapeamento
{
    public class VooPassageiroMapeamento : IEntityTypeConfiguration<VooPassageiro>
    {
        public void Configure(EntityTypeBuilder<VooPassageiro> builder)
        {
            builder.ToTable("VOOS_PASSAGEIROS");
            builder.HasKey(vp => vp.Id);
            builder.Property(vp => vp.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd()
                   .HasColumnName("ID")
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(vp => vp.Status)
                   .HasConversion<int>()
                   .IsRequired()
                   .HasColumnName("STATUS");

            builder.Property(vp => vp.PassageiroId)
                    .IsRequired()
                   .HasColumnName("PASSAGEIRO_ID");

            builder.Property(vp => vp.VooId)
                    .IsRequired()
                    .HasColumnName("VOO_ID");

            builder.HasOne(vp => vp.Passageiro)
                   .WithMany(p => p.VoosPassageiros)
                   .HasForeignKey(vp => vp.PassageiroId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(vp => vp.Voo)
                   .WithMany(v => v.VoosPassageiros)
                   .HasForeignKey(vp => vp.VooId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
