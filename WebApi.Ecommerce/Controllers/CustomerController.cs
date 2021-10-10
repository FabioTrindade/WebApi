using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands.Customer;
using WebApi.Ecommerce.Domain.DTOs.Customer;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CustomerCreateCommand command)
        {
            var result = await _customerService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = (result.Data as CustomerDTO).Id }, result.Data);
        }

        [HttpGet("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _customerService.Handle(new CustomerGetByIdCommand(id));
            return Ok(result);
        }
    }
}
