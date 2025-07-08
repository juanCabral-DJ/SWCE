using Microsoft.AspNetCore.Mvc;
using SWCE.Application.Dtos.ItemCarrito;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Infraestructure.Logging;
using System.Net;

[ApiController]
[Route("api/itemscarrito")]
public class ItemCarritoController : ControllerBase
{
    private readonly IItemCarritoService _itemCarritoService;
    private readonly ILoggerBase<ItemCarritoController> _logger;

    public ItemCarritoController(IItemCarritoService itemCarritoService, ILoggerBase<ItemCarritoController> logger)
    {
        _itemCarritoService = itemCarritoService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddItemToCarrito([FromBody] AddItemCarritoDto dto)
    {
        var result = await _itemCarritoService.AddItemToCarritoAsync(dto);
        return result.IsSuccess ? StatusCode((int)HttpStatusCode.Created, result) : BadRequest(result);
    }

    [HttpPost("update-quantity")]
    public async Task<IActionResult> UpdateItemCantidad([FromBody] UpdateItemCantidadDto dto)
    {
        var result = await _itemCarritoService.UpdateItemCantidadAsync(dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveItemFromCarrito([FromBody] DisableItemCarritoDto dto)
    {
        var result = await _itemCarritoService.RemoveItemFromCarritoAsync(dto.Id);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

}
