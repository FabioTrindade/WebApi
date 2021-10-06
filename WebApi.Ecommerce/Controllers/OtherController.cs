using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet("/v1/[controller]/{zipcode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDTO))]
        public async Task<IActionResult> GetAddressWithZipCode(ZipCodeCommand command)
        {
            var result = _zipCodeProvider.Handle(command);
            return Ok();
        }
    }
}
