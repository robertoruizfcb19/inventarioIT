using Inventario.Domain.Enums;

namespace Inventario.Domain.Entities;

public class Equipo
{
    public Guid Id { get; set; }
    public string CodigoInventario { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? NumeroSerie { get; set; }
    public string? Ubicacion { get; set; }
    public DateTime FechaAdquisicion { get; set; }
    public DateTime? FechaGarantiaFin { get; set; }
    public decimal? ValorCompra { get; set; }
    public EstadoEquipo Estado { get; set; } = EstadoEquipo.Operativo;
    public string? Observaciones { get; set; }

    public ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
    public ICollection<ComponenteInstalado> ComponentesInstalados { get; set; } = new List<ComponenteInstalado>();
}
