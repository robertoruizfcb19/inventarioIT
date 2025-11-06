using Inventario.Application.Common.Interfaces;
using Inventario.Application.Componentes.Dtos;
using Inventario.Application.Equipos.Dtos;
using Inventario.Application.Mantenimientos.Dtos;
using Inventario.Domain.Entities;
using Inventario.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Application.Equipos.Services;

public class EquipoService : IEquipoService
{
    private readonly IInventarioDbContext _context;

    public EquipoService(IInventarioDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<EquipoDto>> GetAsync(EstadoEquipo? estado, string? categoria, CancellationToken cancellationToken)
    {
        var query = _context.Equipos.AsNoTracking().AsQueryable();

        if (estado.HasValue)
        {
            query = query.Where(e => e.Estado == estado.Value);
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            query = query.Where(e => e.Categoria == categoria);
        }

        var equipos = await query
            .OrderBy(e => e.Nombre)
            .Select(e => new EquipoDto(
                e.Id,
                e.CodigoInventario,
                e.Nombre,
                e.Categoria,
                e.Estado,
                e.Ubicacion,
                e.Marca,
                e.Modelo,
                e.NumeroSerie,
                e.FechaAdquisicion,
                e.FechaGarantiaFin))
            .ToListAsync(cancellationToken);

        return equipos;
    }

    public async Task<EquipoDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var equipo = await _context.Equipos
            .AsNoTracking()
            .Include(e => e.ComponentesInstalados)
                .ThenInclude(ci => ci.Componente)
            .Include(e => e.Mantenimientos)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (equipo is null)
        {
            return null;
        }

        var componentes = equipo.ComponentesInstalados
            .OrderByDescending(c => c.FechaInstalacion)
            .Select(ci => new ComponenteAsignadoDto(
                ci.Id,
                ci.Componente?.Nombre ?? string.Empty,
                ci.Componente?.Tipo ?? string.Empty,
                ci.FechaInstalacion,
                ci.FechaRetiro,
                ci.Observaciones))
            .ToList();

        var mantenimientos = equipo.Mantenimientos
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
            .ToList();

        return new EquipoDetailDto(
            equipo.Id,
            equipo.CodigoInventario,
            equipo.Nombre,
            equipo.Categoria,
            equipo.Estado,
            equipo.Descripcion,
            equipo.Ubicacion,
            equipo.Marca,
            equipo.Modelo,
            equipo.NumeroSerie,
            equipo.FechaAdquisicion,
            equipo.FechaGarantiaFin,
            equipo.ValorCompra,
            equipo.Observaciones,
            componentes,
            mantenimientos);
    }

    public async Task<Guid> CreateAsync(CreateEquipoRequest request, CancellationToken cancellationToken)
    {
        var exists = await _context.Equipos
            .AnyAsync(e => e.CodigoInventario == request.CodigoInventario, cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException($"Ya existe un equipo con el código {request.CodigoInventario}.");
        }

        var entity = new Equipo
        {
            Id = Guid.NewGuid(),
            CodigoInventario = request.CodigoInventario.Trim(),
            Nombre = request.Nombre.Trim(),
            Categoria = request.Categoria.Trim(),
            Estado = request.Estado,
            Descripcion = request.Descripcion,
            Ubicacion = request.Ubicacion,
            Marca = request.Marca,
            Modelo = request.Modelo,
            NumeroSerie = request.NumeroSerie,
            FechaAdquisicion = request.FechaAdquisicion,
            FechaGarantiaFin = request.FechaGarantiaFin,
            ValorCompra = request.ValorCompra,
            Observaciones = request.Observaciones
        };

        await _context.Equipos.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateEquipoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _context.Equipos.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        entity.Nombre = request.Nombre.Trim();
        entity.Categoria = request.Categoria.Trim();
        entity.Estado = request.Estado;
        entity.Descripcion = request.Descripcion;
        entity.Ubicacion = request.Ubicacion;
        entity.Marca = request.Marca;
        entity.Modelo = request.Modelo;
        entity.NumeroSerie = request.NumeroSerie;
        entity.FechaAdquisicion = request.FechaAdquisicion;
        entity.FechaGarantiaFin = request.FechaGarantiaFin;
        entity.ValorCompra = request.ValorCompra;
        entity.Observaciones = request.Observaciones;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Equipos
            .Include(e => e.ComponentesInstalados)
            .Include(e => e.Mantenimientos)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        if (entity.ComponentesInstalados.Any(ci => ci.FechaRetiro is null))
        {
            throw new InvalidOperationException("No es posible eliminar un equipo con componentes instalados activos.");
        }

        _context.Equipos.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
