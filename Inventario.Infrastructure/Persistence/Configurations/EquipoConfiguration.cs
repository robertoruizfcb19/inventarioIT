using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Infrastructure.Persistence.Configurations;

public class EquipoConfiguration : IEntityTypeConfiguration<Equipo>
{
    public void Configure(EntityTypeBuilder<Equipo> builder)
    {
        builder.ToTable("Equipos");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CodigoInventario)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Nombre)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Categoria)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Marca).HasMaxLength(100);
        builder.Property(e => e.Modelo).HasMaxLength(100);
        builder.Property(e => e.NumeroSerie).HasMaxLength(150);
        builder.Property(e => e.Ubicacion).HasMaxLength(150);
    }
}
