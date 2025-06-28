using Microsoft.AspNetCore.Mvc;
using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Domain.Base;
using SWCE.Infraestructure.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWCE.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _Services;
        private readonly ILoggerBase<AddressController> _Logger;
        public AddressController(IAddressServices services, ILoggerBase<AddressController> logger)
        {
            _Services = services;
            _Logger = logger;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _Services.GetAllasync(a => a.IsDeleted == false);

            if (result.IsSuccess)
            {
                return Ok(result);
            }else
            {
                _Logger.LogError("Error Fetching Address ");
                return BadRequest(result);
            }
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id)
        {
            var result = await _Services.Getbyidasync(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                _Logger.LogError("Error Fetching Address ");
                return BadRequest(result);
            }
        }

        // GET api/<AddressController>/5
        [HttpGet("Predeterminada")]
        public async Task<IActionResult> GetbyPredeterminada(int id)
        {
            var result = await _Services.GetbyPredeterminada(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                _Logger.LogError("Error Fetching Address ");
                return BadRequest(result);
            }
        }

        // GET api/<AddressController>/5
        [HttpGet("idUser")]
        public async Task<IActionResult> GetbyUserid(int id)
        {
            var result = await _Services.GetbyUserId(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                _Logger.LogError("Error Fetching Address ");
                return BadRequest(result);
            }
        }
        // POST api/<AddressController>
        [HttpPost("CreateAddressDto")]
        public async Task<IActionResult> Post([FromBody] CreateAddressDto dto)
        {
            var result = await _Services.Createasync(dto);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("DisableAddressDto")]
        public async Task<IActionResult> Disable([FromBody] DisableAddressDto dto)
        {
            var result = await _Services.Disableasync(dto);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("UpdateAddressDto")]
        public async Task<IActionResult> Put([FromBody] UpdateAddressDto dto)
        {
            var result = await _Services.Updateasync(dto);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
