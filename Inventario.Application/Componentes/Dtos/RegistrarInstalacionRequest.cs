namespace Inventario.Application.Componentes.Dtos;

public class RegistrarInstalacionRequest
{
    public Guid EquipoId { get; set; }
    public DateTime FechaInstalacion { get; set; }
    public string? Observaciones { get; set; }
}
