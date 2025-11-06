using Inventario.Application.Movimientos.Dtos;
using Inventario.Application.Movimientos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiInventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientosController : ControllerBase
{
    private readonly IMovimientoComponenteService _movimientoService;

    public MovimientosController(IMovimientoComponenteService movimientoService)
    {
        _movimientoService = movimientoService;
    }

    [HttpGet("componentes/{componenteId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<MovimientoComponenteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByComponente(Guid componenteId, CancellationToken cancellationToken)
    {
        var movimientos = await _movimientoService.GetByComponenteAsync(componenteId, cancellationToken);
        return Ok(movimientos);
    }

    [HttpGet("equipos/{equipoId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<MovimientoComponenteDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEquipo(Guid equipoId, CancellationToken cancellationToken)
    {
        var movimientos = await _movimientoService.GetByEquipoAsync(equipoId, cancellationToken);
        return Ok(movimientos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] CreateMovimientoRequest request, CancellationToken cancellationToken)
    {
        var id = await _movimientoService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByComponente), new { componenteId = request.ComponenteId }, id);
    }
}
