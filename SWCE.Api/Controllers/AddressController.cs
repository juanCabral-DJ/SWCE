using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Aplicatition.Dtos.Address;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Infraestructure.Logging;

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
                var result = await _Services.GetAllAsync(a => a.IsDeleted == false);

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
            [HttpGet("Predeterminada/id")]
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
            [HttpGet("idUser/{id}")]
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
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }

            [HttpPost("DisableAddressDto")]
            public async Task<IActionResult> Disable([FromBody] UpdateOrDisableAddressDto dto)
            {
                var result = await _Services.Disableasync(dto);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }

            [HttpPost("UpdateAddressDto")]
            public async Task<IActionResult> Put([FromBody] UpdateOrDisableAddressDto dto)
            {
                var result = await _Services.Updateasync(dto);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
        }
}
