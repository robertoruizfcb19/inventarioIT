using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Application.Common.Interfaces;

public interface IInventarioDbContext
{
    DbSet<Equipo> Equipos { get; }
    DbSet<Componente> Componentes { get; }
    DbSet<Mantenimiento> Mantenimientos { get; }
    DbSet<ComponenteInstalado> ComponentesInstalados { get; }
    DbSet<MovimientoComponente> MovimientosComponentes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
