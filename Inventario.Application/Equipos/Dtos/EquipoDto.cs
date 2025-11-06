using Inventario.Domain.Enums;

namespace Inventario.Application.Equipos.Dtos;

public record EquipoDto(
    Guid Id,
    string CodigoInventario,
    string Nombre,
    string Categoria,
    EstadoEquipo Estado,
    string? Ubicacion,
    string? Marca,
    string? Modelo,
    string? NumeroSerie,
    DateTime FechaAdquisicion,
    DateTime? FechaGarantiaFin
);
