using Inventario.Domain.Enums;

namespace Inventario.Application.Movimientos.Dtos;

public class CreateMovimientoRequest
{
    public Guid ComponenteId { get; set; }
    public Guid? EquipoId { get; set; }
    public Guid? MantenimientoId { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public TipoMovimiento Tipo { get; set; }
    public string? Detalle { get; set; }
    public string? UsuarioRegistro { get; set; }
}
