using Inventario.Application.Componentes.Dtos;
using Inventario.Domain.Enums;

namespace Inventario.Application.Componentes.Services;

public interface IComponenteService
{
    Task<IReadOnlyCollection<ComponenteDto>> GetAsync(EstadoComponente? estado, string? tipo, CancellationToken cancellationToken);
    Task<ComponenteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(CreateComponenteRequest request, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, UpdateComponenteRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> RegistrarInstalacionAsync(Guid componenteId, Guid equipoId, DateTime fechaInstalacion, string? observaciones, CancellationToken cancellationToken);
    Task<bool> RegistrarRetiroAsync(Guid componenteId, DateTime fechaRetiro, string? observaciones, CancellationToken cancellationToken);
}
