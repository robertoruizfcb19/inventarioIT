using Inventario.Domain.Enums;

namespace Inventario.Application.Equipos.Dtos;

public class CreateEquipoRequest
{
    public string CodigoInventario { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public EstadoEquipo Estado { get; set; } = EstadoEquipo.Operativo;
    public string? Descripcion { get; set; }
    public string? Ubicacion { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? NumeroSerie { get; set; }
    public DateTime FechaAdquisicion { get; set; }
    public DateTime? FechaGarantiaFin { get; set; }
    public decimal? ValorCompra { get; set; }
    public string? Observaciones { get; set; }
}
