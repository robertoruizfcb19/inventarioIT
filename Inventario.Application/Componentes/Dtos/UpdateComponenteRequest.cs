using Inventario.Domain.Enums;

namespace Inventario.Application.Componentes.Dtos;

public class UpdateComponenteRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public EstadoComponente Estado { get; set; } = EstadoComponente.Disponible;
    public string? NumeroSerie { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? Descripcion { get; set; }
    public string? UbicacionActual { get; set; }
}
