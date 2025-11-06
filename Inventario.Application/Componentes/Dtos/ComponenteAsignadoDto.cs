namespace Inventario.Application.Componentes.Dtos;

public record ComponenteAsignadoDto(
    Guid Id,
    string Nombre,
    string Tipo,
    DateTime FechaInstalacion,
    DateTime? FechaRetiro,
    string? Observaciones
);
