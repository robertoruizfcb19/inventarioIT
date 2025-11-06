using Inventario.Domain.Enums;

namespace Inventario.Application.Mantenimientos.Dtos;

public record MantenimientoDto(
    Guid Id,
    Guid EquipoId,
    DateTime FechaProgramada,
    DateTime? FechaEjecucion,
    TipoMantenimiento Tipo,
    string? Tecnico,
    string? Descripcion,
    decimal? Costo,
    string? Observaciones,
    DateTime? ProximoMantenimiento
);
