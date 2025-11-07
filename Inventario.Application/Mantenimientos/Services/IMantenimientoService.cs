using Inventario.Application.Mantenimientos.Dtos;

namespace Inventario.Application.Mantenimientos.Services;

public interface IMantenimientoService
{
    Task<IReadOnlyCollection<MantenimientoDto>> GetByEquipoAsync(Guid equipoId, CancellationToken cancellationToken);
    Task<MantenimientoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(CreateMantenimientoRequest request, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, UpdateMantenimientoRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
