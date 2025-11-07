using Inventario.Application.Equipos.Dtos;
using Inventario.Application.Equipos.Services;
using Inventario.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ApiInventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquiposController : ControllerBase
{
    private readonly IEquipoService _equipoService;

    public EquiposController(IEquipoService equipoService)
    {
        _equipoService = equipoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EquipoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] EstadoEquipo? estado, [FromQuery] string? categoria, CancellationToken cancellationToken)
    {
        var equipos = await _equipoService.GetAsync(estado, categoria, cancellationToken);
        return Ok(equipos);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EquipoDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var equipo = await _equipoService.GetByIdAsync(id, cancellationToken);
        return equipo is null ? NotFound() : Ok(equipo);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] CreateEquipoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _equipoService.CreateAsync(request, cancellationToken);
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
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateEquipoRequest request, CancellationToken cancellationToken)
    {
        var updated = await _equipoService.UpdateAsync(id, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var removed = await _equipoService.DeleteAsync(id, cancellationToken);
            return removed ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
