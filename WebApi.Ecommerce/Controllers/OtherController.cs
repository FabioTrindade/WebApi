using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Other;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.Providers;

namespace WebApi.Ecommerce.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api")]
    public class OtherController : ControllerBase
    {
        private readonly IZipCodeProvider _zipCodeProvider;

        public OtherController(IZipCodeProvider zipCodeProvider)
        {
            _zipCodeProvider = zipCodeProvider;
        }

        [HttpGet("/v1/[controller]/address/{zipcode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericCommandResult))]
        public async Task<IActionResult> GetAddressWithZipCode([FromRoute] string zipcode)
        {
            var result = await _zipCodeProvider.Handle(new ZipCodeCommand(zipcode));
            return Ok(result);
        }
        
        [HttpGet("/v1/[controller]/shipping/{zipcode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShippingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericCommandResult))]
        public async Task<IActionResult> GetShippingWithZipCode([FromRoute] string zipcode)
        {
            var result = await _zipCodeProvider.Handle(new ShippingWithZipCodeCommand(zipcode));
            return Ok(result);
        }
    }
}
