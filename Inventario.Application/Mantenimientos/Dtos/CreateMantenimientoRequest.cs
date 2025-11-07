using Inventario.Domain.Enums;

namespace Inventario.Application.Mantenimientos.Dtos;

public class CreateMantenimientoRequest
{
    public Guid EquipoId { get; set; }
    public DateTime FechaProgramada { get; set; }
    public DateTime? FechaEjecucion { get; set; }
    public TipoMantenimiento Tipo { get; set; }
    public string? Tecnico { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Costo { get; set; }
    public string? Observaciones { get; set; }
    public DateTime? ProximoMantenimiento { get; set; }
}
