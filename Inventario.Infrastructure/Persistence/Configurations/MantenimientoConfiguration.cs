using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Infrastructure.Persistence.Configurations;

public class MantenimientoConfiguration : IEntityTypeConfiguration<Mantenimiento>
{
    public void Configure(EntityTypeBuilder<Mantenimiento> builder)
    {
        builder.ToTable("Mantenimientos");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Tecnico).HasMaxLength(150);
        builder.Property(m => m.Descripcion).HasMaxLength(1000);
        builder.Property(m => m.Observaciones).HasMaxLength(1000);

        builder.HasOne(m => m.Equipo)
            .WithMany(e => e.Mantenimientos)
            .HasForeignKey(m => m.EquipoId);
    }
}
