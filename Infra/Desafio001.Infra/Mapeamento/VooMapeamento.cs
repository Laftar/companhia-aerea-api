using Desafio001.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio001.Infra.Mapeamento
{
    public class VooMapeamento : IEntityTypeConfiguration<Voo>
    {
        public void Configure(EntityTypeBuilder<Voo> builder)
        {
            builder.ToTable("VOOS");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()")
                   .HasColumnName("ID");

            builder.Property(v => v.Status)
                   .HasConversion<int>()
                   .IsRequired()
                   .HasColumnName("STATUS");

            builder.Property(v => v.AviaoId)
                   .IsRequired()
                   .HasColumnName("AVIAO_ID");

            builder.HasOne(v => v.Aviao)
                   .WithMany(a => a.Voos)
                   .HasForeignKey(v => v.AviaoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(v => v.PilotoId)
                   .IsRequired()
                   .HasColumnName("PILOTO_ID");

            builder.HasOne(v => v.Piloto)
                   .WithMany(p => p.Voos)
                   .HasForeignKey(v => v.PilotoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(v => v.DataVoo).IsRequired();
            builder.Property(v => v.HorarioPrevSaida).IsRequired();
            builder.Property(v => v.HorarioPrevChegada).IsRequired();
            builder.Property(v => v.HorarioRealSaida);
            builder.Property(v => v.HorarioRealChegada);
            builder.Property(v => v.QtdPassagensVendidas).IsRequired();
            builder.Property(v => v.QtdPassageirosPresentes).IsRequired();

            builder.HasMany(v => v.VoosPassageiros)
                   .WithOne(vp => vp.Voo)
                   .HasForeignKey(vp => vp.VooId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
