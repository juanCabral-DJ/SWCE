using Microsoft.AspNetCore.Mvc;
using SWCE.Aplication.DTOs.Envio;
using SWCE.Aplication.Interfaces.Services;
using SWCE.Infraestructure.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWCE.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        private readonly IEnvioServices _envioService;
        private readonly ILoggerBase<EnvioController> _logger;

        public EnvioController(IEnvioServices envioService, ILoggerBase<EnvioController> logger)
        {
            _envioService = envioService;
            _logger = logger;
        }

        // GET: api/<EnvioController>
        [HttpGet("GetEnvios")]
        public async Task<IActionResult> get()
        {
            var result = await _envioService.GetAllAsync();

            if (result.IsSuccess)
            {
                _logger.LogInformation("Envio retrieved successfully");
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error fetching envio");
                return BadRequest(result);
            }
        }

        // GET api/<EnvioController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _envioService.GetByIdAsync(id);

            if (result.IsSuccess)
            { 
                _logger.LogInformation("Envio retrieved successfully");
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error fetching envio");
                return BadRequest(result);
            }
        }

        // POST api/<EnvioController>
        [HttpPost("CreateEnvio")]
        public async Task<IActionResult> Post([FromBody] CreateEnvioDto dto)
        {
            var result = await _envioService.CreateAsync(dto);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Envio created successfully", dto);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error creating envio");
                return BadRequest(result);
            }
        }
        
        // POST api/<EnvioController>
        [HttpPost("UpdateEnvioDto")]
        public async Task<IActionResult> Put([FromBody] UpdateEnvioDto dto)
        {
            var result = await _envioService.UpdateAsync(dto);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Envio updated successfully", dto);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error updating envio");
                return BadRequest(result);
            }
         }

    }
}
