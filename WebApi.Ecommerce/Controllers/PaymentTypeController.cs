using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands.PaymentType;
using WebApi.Ecommerce.Domain.DTOs.PaymentType;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api")]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [HttpPost("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PaymentTypeCreateCommand command)
        {
            var result = await _paymentTypeService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = (result.Data as PaymentTypeDTO).Id }, result.Data);
        }

        [HttpGet("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypeDTO))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _paymentTypeService.Handle(new PaymentTypeGetByIdCommand(id));
            return Ok(result);
        }

        [HttpPatch("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] PaymentTypeUpdateByIdCommand command)
        {
            command.Id = id;
            var result = await _paymentTypeService.Handle(command);
            return Ok(result);
        }
    }
}
