using Inventario.Application.Componentes.Dtos;
using Inventario.Application.Componentes.Services;
using Inventario.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ApiInventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComponentesController : ControllerBase
{
    private readonly IComponenteService _componenteService;

    public ComponentesController(IComponenteService componenteService)
    {
        _componenteService = componenteService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ComponenteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] EstadoComponente? estado, [FromQuery] string? tipo, CancellationToken cancellationToken)
    {
        var componentes = await _componenteService.GetAsync(estado, tipo, cancellationToken);
        return Ok(componentes);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ComponenteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var componente = await _componenteService.GetByIdAsync(id, cancellationToken);
        return componente is null ? NotFound() : Ok(componente);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] CreateComponenteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _componenteService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateComponenteRequest request, CancellationToken cancellationToken)
    {
        var updated = await _componenteService.UpdateAsync(id, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var removed = await _componenteService.DeleteAsync(id, cancellationToken);
            return removed ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("{id:guid}/instalaciones")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegistrarInstalacion(Guid id, [FromBody] RegistrarInstalacionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _componenteService.RegistrarInstalacionAsync(id, request.EquipoId, request.FechaInstalacion, request.Observaciones, cancellationToken);
            return result ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("{id:guid}/retiros")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegistrarRetiro(Guid id, [FromBody] RegistrarRetiroRequest request, CancellationToken cancellationToken)
    {
        var result = await _componenteService.RegistrarRetiroAsync(id, request.FechaRetiro, request.Observaciones, cancellationToken);
        return result ? NoContent() : NotFound();
    }
}
