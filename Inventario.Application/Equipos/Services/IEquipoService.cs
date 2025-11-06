using Inventario.Application.Equipos.Dtos;
using Inventario.Domain.Enums;

namespace Inventario.Application.Equipos.Services;

public interface IEquipoService
{
    Task<IReadOnlyCollection<EquipoDto>> GetAsync(EstadoEquipo? estado, string? categoria, CancellationToken cancellationToken);
    Task<EquipoDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(CreateEquipoRequest request, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, UpdateEquipoRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
