using Inventario.Application.Movimientos.Dtos;

namespace Inventario.Application.Movimientos.Services;

public interface IMovimientoComponenteService
{
    Task<IReadOnlyCollection<MovimientoComponenteDto>> GetByComponenteAsync(Guid componenteId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<MovimientoComponenteDto>> GetByEquipoAsync(Guid equipoId, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(CreateMovimientoRequest request, CancellationToken cancellationToken);
}
