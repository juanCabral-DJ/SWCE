using Microsoft.AspNetCore.Mvc;
using SWCE.Application.Dtos.AdministracionModule.CuponMontoFijoDtos;
using SWCE.Application.Interfaces.Services;
using SWCE.Domain.Base;

namespace SWCE.Api.Controllers.AdministracionModule
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuponMontoFijoController : ControllerBase
    {
        private readonly ICuponMontoFijoServices _service;

        public CuponMontoFijoController(ICuponMontoFijoServices service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllasync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetbyId(id);
            return HandleResult(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCuponMontoFijoDto dto)
        {
            var result = await _service.Createasync(dto);
            return HandleResult(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCuponMontoFijoDto dto)
        {
            var result = await _service.Updateasync(dto);
            return HandleResult(result);
        }

        private IActionResult HandleResult(OperationResult result)
        {
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("disable/{id}")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Disable(int id)
        {
            var result = await _service.Disableasync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

    }
}
