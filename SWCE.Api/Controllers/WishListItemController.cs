using Microsoft.AspNetCore.Mvc; 
using SWCE.Aplicatition.Dtos.WishListItem;
using SWCE.Aplicatition.Interfaces.Services;
using SWCE.Infraestructure.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWCE.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListItemController : ControllerBase
    {
         public readonly ILoggerBase<WishListItemController> _Logger;
        public readonly IWishListItemServices _services;

        public WishListItemController(ILoggerBase<WishListItemController> logger, IWishListItemServices services)
        {
            _Logger = logger;
            _services = services;
        }


        // GET: api/<WishListItemController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _services.GetAllAsync(a => a.IsDeleted == false);

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

        // GET api/<WishListItemController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id)
        {
            var result = await _services.Getbyidasync(id);

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
        [HttpGet("Item/{id}")]
        public async Task<IActionResult> GetbyUserid(int id)
        {
            var result = await _services.GetbyUserId(id);

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

        // POST api/<WishListItemController>
        [HttpPost("CreateItemDto")]
        public async Task<IActionResult> Post([FromBody] CreateItemDto dto)
        {
            var result = await _services.Createasync(dto);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // POST api/<WishListItemController>
        [HttpPost("DisableItemDto")]
        public async Task<IActionResult> Disable([FromBody] DisableItemDto dto)
        {
            var result = await _services.Disableasync(dto);

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
