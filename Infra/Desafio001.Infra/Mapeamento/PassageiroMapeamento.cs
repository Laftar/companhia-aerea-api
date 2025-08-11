using Desafio001.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio001.Infra.Mapeamento
{
    public class PassageiroMapeamento : IEntityTypeConfiguration<Passageiro>
    {
        public void Configure(EntityTypeBuilder<Passageiro> builder)
        {
            builder.ToTable("PASSAGEIROS");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd()
                   .HasDefaultValueSql("NEWSEQUENTIALID()")
                   .HasColumnName("ID");

            builder.Property(p => p.Nome)
                   .IsRequired().HasMaxLength(150)
                   .HasColumnName("NOME");
            
            builder.Property(p => p.Documento)
                   .IsRequired()
                   .HasMaxLength(11)
                   .HasColumnName("DOCUMENTO");

            builder.Property(p => p.DataNascimento)
                .IsRequired()
                .HasColumnName("DATA_NASCIMENTO");

            builder.HasMany(p => p.VoosPassageiros)
                   .WithOne(vp => vp.Passageiro)
                   .HasForeignKey(vp => vp.PassageiroId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
