using Inventario.Application.Componentes.Dtos;
using Inventario.Application.Mantenimientos.Dtos;
using Inventario.Domain.Enums;

namespace Inventario.Application.Equipos.Dtos;

public record EquipoDetailDto(
    Guid Id,
    string CodigoInventario,
    string Nombre,
    string Categoria,
    EstadoEquipo Estado,
    string? Descripcion,
    string? Ubicacion,
    string? Marca,
    string? Modelo,
    string? NumeroSerie,
    DateTime FechaAdquisicion,
    DateTime? FechaGarantiaFin,
    decimal? ValorCompra,
    string? Observaciones,
    IReadOnlyCollection<ComponenteAsignadoDto> Componentes,
    IReadOnlyCollection<MantenimientoDto> Mantenimientos
);
