using Microsoft.AspNetCore.Mvc;
using SWCE.Application.Dtos.AdministracionModule.CategoriaDto;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Entities;
using System.Linq.Expressions;

namespace SWCE.Api.Controllers.AdministracionModule
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServices _categoriaServices;

        public CategoriaController(ICategoriaServices categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoriaServices.GetbyId(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("Getall")]
        public async Task<IActionResult> GetAll()
        {
            // Retorna todas las categorías incluyendo inactivas si no hay filtro aplicado
            Expression<Func<Categoria, bool>> filter = _ => true;
            var result = await _categoriaServices.GetAllasync(filter);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("CreateCategoria")]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos de entrada no válidos.");

            var result = await _categoriaServices.Createasync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("UpdateCategoria")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoriaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos de entrada no válidos.");

            var result = await _categoriaServices.Updateasync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("GetActivas")]
        public async Task<IActionResult> GetActivas()
        {
            try
            {
                var result = await _categoriaServices.ObtenerActivasAsync();

                if (result == null || !result.Any())
                    return NotFound("No se encontraron categorías activas");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno al obtener categorías activas");
            }
        }

        [HttpPost("Deshabilitar/{id}")]
        public async Task<IActionResult> Deshabilitar(int id)
        {
            var result = await _categoriaServices.DisableAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
