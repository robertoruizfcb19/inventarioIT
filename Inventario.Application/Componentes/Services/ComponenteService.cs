using Inventario.Application.Common.Interfaces;
using Inventario.Application.Componentes.Dtos;
using Inventario.Domain.Entities;
using Inventario.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Application.Componentes.Services;

public class ComponenteService : IComponenteService
{
    private readonly IInventarioDbContext _context;

    public ComponenteService(IInventarioDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ComponenteDto>> GetAsync(EstadoComponente? estado, string? tipo, CancellationToken cancellationToken)
    {
        var query = _context.Componentes.AsNoTracking().AsQueryable();

        if (estado.HasValue)
        {
            query = query.Where(c => c.Estado == estado.Value);
        }

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            query = query.Where(c => c.Tipo == tipo);
        }

        return await query
            .OrderBy(c => c.Nombre)
            .Select(c => new ComponenteDto(
                c.Id,
                c.Nombre,
                c.Tipo,
                c.Estado,
                c.NumeroSerie,
                c.Marca,
                c.Modelo,
                c.UbicacionActual))
            .ToListAsync(cancellationToken);
    }

    public async Task<ComponenteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Componentes
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new ComponenteDto(
                c.Id,
                c.Nombre,
                c.Tipo,
                c.Estado,
                c.NumeroSerie,
                c.Marca,
                c.Modelo,
                c.UbicacionActual))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateAsync(CreateComponenteRequest request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.NumeroSerie))
        {
            var exists = await _context.Componentes
                .AnyAsync(c => c.NumeroSerie == request.NumeroSerie, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException($"Ya existe un componente con el número de serie {request.NumeroSerie}.");
            }
        }

        var entity = new Componente
        {
            Id = Guid.NewGuid(),
            Nombre = request.Nombre.Trim(),
            Tipo = request.Tipo.Trim(),
            Estado = request.Estado,
            NumeroSerie = request.NumeroSerie,
            Marca = request.Marca,
            Modelo = request.Modelo,
            Descripcion = request.Descripcion,
            UbicacionActual = request.UbicacionActual
        };

        await _context.Componentes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateComponenteRequest request, CancellationToken cancellationToken)
    {
        var entity = await _context.Componentes.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Nombre = request.Nombre.Trim();
        entity.Tipo = request.Tipo.Trim();
        entity.Estado = request.Estado;
        entity.NumeroSerie = request.NumeroSerie;
        entity.Marca = request.Marca;
        entity.Modelo = request.Modelo;
        entity.Descripcion = request.Descripcion;
        entity.UbicacionActual = request.UbicacionActual;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Componentes
            .Include(c => c.Instalaciones)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        if (entity.Instalaciones.Any(i => i.FechaRetiro is null))
        {
            throw new InvalidOperationException("No es posible eliminar un componente que está instalado en un equipo.");
        }

        _context.Componentes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RegistrarInstalacionAsync(Guid componenteId, Guid equipoId, DateTime fechaInstalacion, string? observaciones, CancellationToken cancellationToken)
    {
        var componente = await _context.Componentes
            .Include(c => c.Instalaciones)
            .FirstOrDefaultAsync(c => c.Id == componenteId, cancellationToken);

        if (componente is null)
        {
            return false;
        }

        if (componente.Instalaciones.Any(i => i.FechaRetiro is null))
        {
            throw new InvalidOperationException("El componente ya está instalado en un equipo.");
        }

        var equipo = await _context.Equipos.FirstOrDefaultAsync(e => e.Id == equipoId, cancellationToken);
        if (equipo is null)
        {
            throw new InvalidOperationException("El equipo indicado no existe.");
        }

        componente.Estado = EstadoComponente.Instalado;
        componente.UbicacionActual = equipo.Ubicacion ?? equipo.Nombre;

        var instalacion = new ComponenteInstalado
        {
            Id = Guid.NewGuid(),
            ComponenteId = componenteId,
            EquipoId = equipoId,
            FechaInstalacion = fechaInstalacion,
            Observaciones = observaciones
        };

        await _context.ComponentesInstalados.AddAsync(instalacion, cancellationToken);

        var movimiento = new MovimientoComponente
        {
            Id = Guid.NewGuid(),
            ComponenteId = componenteId,
            EquipoId = equipoId,
            FechaMovimiento = fechaInstalacion,
            Tipo = TipoMovimiento.Instalacion,
            Detalle = observaciones,
            UsuarioRegistro = "system"
        };

        await _context.MovimientosComponentes.AddAsync(movimiento, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RegistrarRetiroAsync(Guid componenteId, DateTime fechaRetiro, string? observaciones, CancellationToken cancellationToken)
    {
        var instalacion = await _context.ComponentesInstalados
            .Include(ci => ci.Componente)
            .Where(ci => ci.ComponenteId == componenteId && ci.FechaRetiro == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (instalacion is null)
        {
            return false;
        }

        instalacion.FechaRetiro = fechaRetiro;
        instalacion.Observaciones = observaciones;

        if (instalacion.Componente is not null)
        {
            instalacion.Componente.Estado = EstadoComponente.Disponible;
            instalacion.Componente.UbicacionActual = "Almacén";
        }

        var movimiento = new MovimientoComponente
        {
            Id = Guid.NewGuid(),
            ComponenteId = componenteId,
            EquipoId = instalacion.EquipoId,
            FechaMovimiento = fechaRetiro,
            Tipo = TipoMovimiento.Retiro,
            Detalle = observaciones,
            UsuarioRegistro = "system"
        };

        await _context.MovimientosComponentes.AddAsync(movimiento, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
