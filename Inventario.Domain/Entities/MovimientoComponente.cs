using Inventario.Domain.Enums;

namespace Inventario.Domain.Entities;

public class MovimientoComponente
{
    public Guid Id { get; set; }
    public Guid ComponenteId { get; set; }
    public Guid? EquipoId { get; set; }
    public Guid? MantenimientoId { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public TipoMovimiento Tipo { get; set; }
    public string? Detalle { get; set; }
    public string? UsuarioRegistro { get; set; }

    public Componente? Componente { get; set; }
    public Equipo? Equipo { get; set; }
    public Mantenimiento? Mantenimiento { get; set; }
}
