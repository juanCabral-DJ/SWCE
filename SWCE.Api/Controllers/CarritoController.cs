using Microsoft.AspNetCore.Mvc;
using SWCE.Application.Dtos.Carrito;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Infraestructure.Logging;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class CarritoController : ControllerBase
{
    private readonly ICarritoService _carritoService;
    private readonly ILoggerBase<CarritoController> _logger;

    public CarritoController(ICarritoService carritoService, ILoggerBase<CarritoController> logger)
    {
        _carritoService = carritoService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCarritos()
    {
        var result = await _carritoService.GetAllAsync();
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarritoById(int id)
    {
        var result = await _carritoService.GetByIdAsync(id);
        if (result.IsSuccess && result.Data != null)
            return Ok(result);

        return NotFound(OperationResult.Failure($"Carrito with ID {id} not found."));
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetActiveCarritoByUserId(int userId)
    {
        var result = await _carritoService.GetActiveCarritoByUserIdAsync(userId);
        if (result.IsSuccess && result.Data != null)
            return Ok(result);

        return NotFound(OperationResult.Failure($"No active carrito found for user ID {userId}."));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCarrito([FromBody] CreateCarritoDto dto)
    {
        var result = await _carritoService.CreateAsync(dto);
        return result.IsSuccess ? StatusCode((int)HttpStatusCode.Created, result) : BadRequest(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateCarrito([FromBody] UpdateCarritoDto dto)
    {
        var result = await _carritoService.UpdateAsync(dto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost("disable")]
    public async Task<IActionResult> DisableCarrito([FromBody] DisableCarritoDto dto)
    {
        var result = await _carritoService.DisableAsync(dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("clear/{carritoId}")]
    public async Task<IActionResult> ClearCarrito(int carritoId)
    {
        var result = await _carritoService.ClearCarritoAsync(carritoId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
