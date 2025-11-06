using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Infrastructure.Persistence.Configurations;

public class ComponenteInstaladoConfiguration : IEntityTypeConfiguration<ComponenteInstalado>
{
    public void Configure(EntityTypeBuilder<ComponenteInstalado> builder)
    {
        builder.ToTable("ComponentesInstalados");
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Observaciones).HasMaxLength(500);

        builder.HasOne(ci => ci.Equipo)
            .WithMany(e => e.ComponentesInstalados)
            .HasForeignKey(ci => ci.EquipoId);

        builder.HasOne(ci => ci.Componente)
            .WithMany(c => c.Instalaciones)
            .HasForeignKey(ci => ci.ComponenteId);
    }
}
