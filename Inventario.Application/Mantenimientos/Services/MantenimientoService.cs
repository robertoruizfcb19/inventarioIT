using Inventario.Application.Common.Interfaces;
using Inventario.Application.Mantenimientos.Dtos;
using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Application.Mantenimientos.Services;

public class MantenimientoService : IMantenimientoService
{
    private readonly IInventarioDbContext _context;

    public MantenimientoService(IInventarioDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<MantenimientoDto>> GetByEquipoAsync(Guid equipoId, CancellationToken cancellationToken)
    {
        return await _context.Mantenimientos
            .AsNoTracking()
            .Where(m => m.EquipoId == equipoId)
            .OrderByDescending(m => m.FechaProgramada)
            .Select(m => new MantenimientoDto(
                m.Id,
                m.EquipoId,
                m.FechaProgramada,
                m.FechaEjecucion,
                m.Tipo,
                m.Tecnico,
                m.Descripcion,
                m.Costo,
                m.Observaciones,
                m.ProximoMantenimiento))
            .ToListAsync(cancellationToken);
    }

    public async Task<MantenimientoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Mantenimientos
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new MantenimientoDto(
                m.Id,
                m.EquipoId,
                m.FechaProgramada,
                m.FechaEjecucion,
                m.Tipo,
                m.Tecnico,
                m.Descripcion,
                m.Costo,
                m.Observaciones,
                m.ProximoMantenimiento))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateAsync(CreateMantenimientoRequest request, CancellationToken cancellationToken)
    {
        var entity = new Mantenimiento
        {
            Id = Guid.NewGuid(),
            EquipoId = request.EquipoId,
            FechaProgramada = request.FechaProgramada,
            FechaEjecucion = request.FechaEjecucion,
            Tipo = request.Tipo,
            Tecnico = request.Tecnico,
            Descripcion = request.Descripcion,
            Costo = request.Costo,
            Observaciones = request.Observaciones,
            ProximoMantenimiento = request.ProximoMantenimiento
        };

        await _context.Mantenimientos.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateMantenimientoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _context.Mantenimientos.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        entity.FechaProgramada = request.FechaProgramada;
        entity.FechaEjecucion = request.FechaEjecucion;
        entity.Tipo = request.Tipo;
        entity.Tecnico = request.Tecnico;
        entity.Descripcion = request.Descripcion;
        entity.Costo = request.Costo;
        entity.Observaciones = request.Observaciones;
        entity.ProximoMantenimiento = request.ProximoMantenimiento;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Mantenimientos.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        _context.Mantenimientos.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
