// ProductoController.cs
using Microsoft.AspNetCore.Mvc;
using SWCE.Application.Dtos.AdministracionModule.ProductoDtos;
using SWCE.Application.Interfaces.Services;
using SWCE.Application.Services;
using SWCE.Infraestructure.Logging;

namespace SWCE.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _services;
        private readonly ILoggerBase<ProductoController> _logger;

        public ProductoController(IProductoServices services, ILoggerBase<ProductoController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpGet ("GetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _services.GetAllasync(p => true);
            if (result.IsSuccess)
                return Ok(result);

            _logger.LogError("Error fetching Productos");
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _services.GetbyId(id);
            if (result.IsSuccess)
                return Ok(result);

            _logger.LogError("Error fetching Producto by ID");
            return BadRequest(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] CreateProductoDto dto)
        {
            var result = await _services.Createasync(dto);
            if (result.IsSuccess)
                return Ok(result);

            _logger.LogError("Error creating Producto");
            return BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] UpdateProductoDto dto)
        {
            var result = await _services.Updateasync(dto);
            if (result.IsSuccess)
                return Ok(result);

            _logger.LogError("Error updating Producto");
            return BadRequest(result);
        }

        [HttpPost("disable/{id}")]
        public async Task<IActionResult> DisableProducto(int id)
        {
            var result = await _services.DisableAsync(id);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message, data = result.Data });
        }
    }
}
