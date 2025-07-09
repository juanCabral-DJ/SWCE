using Microsoft.AspNetCore.Mvc;
using SWCE.Application.Dtos.AdministracionModule.CuponPorcentajeDtos;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;

namespace SWCE.Api.Controllers.AdministracionModule
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuponPorcentajeController : ControllerBase
    {
        private readonly ICuponPorcentajeServices _service;

        public CuponPorcentajeController(ICuponPorcentajeServices service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllasync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetbyId(id);
            return HandleResult(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCuponPorcentajeDto dto)
        {
            var result = await _service.Createasync(dto);
            return HandleResult(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCuponPorcentajeDto dto)
        {
            var result = await _service.Updateasync(dto);
            return HandleResult(result);
        }

        [HttpPost("disable/{id}")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Disable(int id)
        {
            var result = await _service.DisableAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        private IActionResult HandleResult(OperationResult result)
        {
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
