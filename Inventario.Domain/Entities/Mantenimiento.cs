using Inventario.Domain.Enums;

namespace Inventario.Domain.Entities;

public class Mantenimiento
{
    public Guid Id { get; set; }
    public Guid EquipoId { get; set; }
    public DateTime FechaProgramada { get; set; }
    public DateTime? FechaEjecucion { get; set; }
    public TipoMantenimiento Tipo { get; set; }
    public string? Tecnico { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Costo { get; set; }
    public string? Observaciones { get; set; }
    public DateTime? ProximoMantenimiento { get; set; }

    public Equipo? Equipo { get; set; }
    public ICollection<MovimientoComponente> Movimientos { get; set; } = new List<MovimientoComponente>();
}
