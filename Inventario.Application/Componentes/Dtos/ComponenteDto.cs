using Inventario.Domain.Enums;

namespace Inventario.Application.Componentes.Dtos;

public record ComponenteDto(
    Guid Id,
    string Nombre,
    string Tipo,
    EstadoComponente Estado,
    string? NumeroSerie,
    string? Marca,
    string? Modelo,
    string? UbicacionActual
);
