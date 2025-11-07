using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Infrastructure.Persistence.Configurations;

public class ComponenteConfiguration : IEntityTypeConfiguration<Componente>
{
    public void Configure(EntityTypeBuilder<Componente> builder)
    {
        builder.ToTable("Componentes");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.Tipo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.NumeroSerie).HasMaxLength(150);
        builder.Property(c => c.Marca).HasMaxLength(100);
        builder.Property(c => c.Modelo).HasMaxLength(100);
        builder.Property(c => c.UbicacionActual).HasMaxLength(150);
    }
}
