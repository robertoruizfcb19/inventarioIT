namespace Inventario.Domain.Entities;

public class ComponenteInstalado
{
    public Guid Id { get; set; }
    public Guid EquipoId { get; set; }
    public Guid ComponenteId { get; set; }
    public DateTime FechaInstalacion { get; set; }
    public DateTime? FechaRetiro { get; set; }
    public string? Observaciones { get; set; }

    public Equipo? Equipo { get; set; }
    public Componente? Componente { get; set; }
}
