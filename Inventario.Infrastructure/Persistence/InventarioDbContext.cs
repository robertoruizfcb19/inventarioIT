using Inventario.Application.Common.Interfaces;
using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.Persistence;

public class InventarioDbContext : DbContext, IInventarioDbContext
{
    public InventarioDbContext(DbContextOptions<InventarioDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<Componente> Componentes => Set<Componente>();
    public DbSet<Mantenimiento> Mantenimientos => Set<Mantenimiento>();
    public DbSet<ComponenteInstalado> ComponentesInstalados => Set<ComponenteInstalado>();
    public DbSet<MovimientoComponente> MovimientosComponentes => Set<MovimientoComponente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventarioDbContext).Assembly);
    }
}
