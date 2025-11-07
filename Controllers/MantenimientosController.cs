using Inventario.Application.Mantenimientos.Dtos;
using Inventario.Application.Mantenimientos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiInventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MantenimientosController : ControllerBase
{
    private readonly IMantenimientoService _mantenimientoService;

    public MantenimientosController(IMantenimientoService mantenimientoService)
    {
        _mantenimientoService = mantenimientoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MantenimientoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] Guid? equipoId, CancellationToken cancellationToken)
    {
        if (!equipoId.HasValue)
        {
            return BadRequest(new { message = "Debe especificar el identificador del equipo." });
        }

        var mantenimientos = await _mantenimientoService.GetByEquipoAsync(equipoId.Value, cancellationToken);
        return Ok(mantenimientos);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MantenimientoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var mantenimiento = await _mantenimientoService.GetByIdAsync(id, cancellationToken);
        return mantenimiento is null ? NotFound() : Ok(mantenimiento);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] CreateMantenimientoRequest request, CancellationToken cancellationToken)
    {
        var id = await _mantenimientoService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateMantenimientoRequest request, CancellationToken cancellationToken)
    {
        var updated = await _mantenimientoService.UpdateAsync(id, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var removed = await _mantenimientoService.DeleteAsync(id, cancellationToken);
        return removed ? NoContent() : NotFound();
    }
}
