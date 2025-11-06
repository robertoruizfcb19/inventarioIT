using Inventario.Domain.Enums;

namespace Inventario.Application.Movimientos.Dtos;

public record MovimientoComponenteDto(
    Guid Id,
    Guid ComponenteId,
    Guid? EquipoId,
    Guid? MantenimientoId,
    DateTime FechaMovimiento,
    TipoMovimiento Tipo,
    string? Detalle,
    string? UsuarioRegistro
);
