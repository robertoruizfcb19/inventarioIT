using Inventario.Application.Common.Interfaces;
using Inventario.Application.Componentes.Services;
using Inventario.Application.Equipos.Services;
using Inventario.Application.Mantenimientos.Services;
using Inventario.Application.Movimientos.Services;
using Inventario.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventario.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");

        services.AddDbContext<InventarioDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IInventarioDbContext>(sp => sp.GetRequiredService<InventarioDbContext>());

        services.AddScoped<IEquipoService, EquipoService>();
        services.AddScoped<IComponenteService, ComponenteService>();
        services.AddScoped<IMantenimientoService, MantenimientoService>();
        services.AddScoped<IMovimientoComponenteService, MovimientoComponenteService>();

        return services;
    }
}
