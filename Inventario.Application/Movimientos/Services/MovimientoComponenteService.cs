using Inventario.Application.Common.Interfaces;
using Inventario.Application.Movimientos.Dtos;
using Inventario.Domain.Entities;
using Inventario.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Application.Movimientos.Services;

public class MovimientoComponenteService : IMovimientoComponenteService
{
    private readonly IInventarioDbContext _context;

    public MovimientoComponenteService(IInventarioDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<MovimientoComponenteDto>> GetByComponenteAsync(Guid componenteId, CancellationToken cancellationToken)
    {
        return await _context.MovimientosComponentes
            .AsNoTracking()
            .Where(m => m.ComponenteId == componenteId)
            .OrderByDescending(m => m.FechaMovimiento)
            .Select(m => new MovimientoComponenteDto(
                m.Id,
                m.ComponenteId,
                m.EquipoId,
                m.MantenimientoId,
                m.FechaMovimiento,
                m.Tipo,
                m.Detalle,
                m.UsuarioRegistro))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<MovimientoComponenteDto>> GetByEquipoAsync(Guid equipoId, CancellationToken cancellationToken)
    {
        return await _context.MovimientosComponentes
            .AsNoTracking()
            .Where(m => m.EquipoId == equipoId)
            .OrderByDescending(m => m.FechaMovimiento)
            .Select(m => new MovimientoComponenteDto(
                m.Id,
                m.ComponenteId,
                m.EquipoId,
                m.MantenimientoId,
                m.FechaMovimiento,
                m.Tipo,
                m.Detalle,
                m.UsuarioRegistro))
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> CreateAsync(CreateMovimientoRequest request, CancellationToken cancellationToken)
    {
        if (request.Tipo == TipoMovimiento.Instalacion && request.EquipoId is null)
        {
            throw new InvalidOperationException("Los movimientos de instalación requieren el identificador del equipo.");
        }

        var entity = new MovimientoComponente
        {
            Id = Guid.NewGuid(),
            ComponenteId = request.ComponenteId,
            EquipoId = request.EquipoId,
            MantenimientoId = request.MantenimientoId,
            FechaMovimiento = request.FechaMovimiento,
            Tipo = request.Tipo,
            Detalle = request.Detalle,
            UsuarioRegistro = request.UsuarioRegistro
        };

        await _context.MovimientosComponentes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
