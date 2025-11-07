using Inventario.Domain.Enums;

namespace Inventario.Domain.Entities;

public class Componente
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string? NumeroSerie { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? Descripcion { get; set; }
    public EstadoComponente Estado { get; set; } = EstadoComponente.Disponible;
    public string? UbicacionActual { get; set; }

    public ICollection<MovimientoComponente> Movimientos { get; set; } = new List<MovimientoComponente>();
    public ICollection<ComponenteInstalado> Instalaciones { get; set; } = new List<ComponenteInstalado>();
}
