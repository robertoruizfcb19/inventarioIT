using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Infrastructure.Persistence.Configurations;

public class MovimientoComponenteConfiguration : IEntityTypeConfiguration<MovimientoComponente>
{
    public void Configure(EntityTypeBuilder<MovimientoComponente> builder)
    {
        builder.ToTable("MovimientosComponentes");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Detalle).HasMaxLength(500);
        builder.Property(m => m.UsuarioRegistro).HasMaxLength(150);

        builder.HasOne(m => m.Componente)
            .WithMany(c => c.Movimientos)
            .HasForeignKey(m => m.ComponenteId);

        builder.HasOne(m => m.Equipo)
            .WithMany()
            .HasForeignKey(m => m.EquipoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(m => m.Mantenimiento)
            .WithMany(mnt => mnt.Movimientos)
            .HasForeignKey(m => m.MantenimientoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
