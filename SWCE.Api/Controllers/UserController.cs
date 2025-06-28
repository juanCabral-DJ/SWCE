using Microsoft.AspNetCore.Mvc;
using SWCE.Aplicatition.Dtos.User;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Infraestructure.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWCE.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _Services;
        private readonly ILoggerBase<UserController> _Logger;
        public UserController(IUserServices services, ILoggerBase<UserController> logger)
        {
            _Services = services;
            _Logger = logger;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _Services.GetAllasync();

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
            var result = await _Services.GetbyIdasync(id);

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
        [HttpGet("Email")]
        public async Task<IActionResult> GetbyEmail(string email)
        {
            var result = await _Services.GetByEmail(email);

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

        // POST api/<UserController>
        [HttpPost("CreateUserDto")]
        public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
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

        [HttpPost("DisableUserDto")]
        public async Task<IActionResult> Disable([FromBody] DisableUserDto dto)
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

        [HttpPost("UpdateUserDto")]
        public async Task<IActionResult> Put([FromBody] UpdateUserDto dto)
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
