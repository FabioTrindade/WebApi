using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands.PaymentStatus;
using WebApi.Ecommerce.Domain.DTOs.PaymentStatus;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api")]
    public class PaymentStatusController : ControllerBase
    {
        private readonly IPaymentStatusService _paymentStatusService;

        public PaymentStatusController(IPaymentStatusService paymentStatusService)
        {
            _paymentStatusService = paymentStatusService;
        }

        [HttpPost("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PaymentStatusCreateCommand command)
        {
            var result = await _paymentStatusService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = (result.Data as PaymentStatusDTO).Id }, result.Data);
        }

        [HttpGet("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentStatusDTO))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _paymentStatusService.Handle(new PaymentStatusGetByIdCommand(id));
            return Ok(result);
        }

        [HttpGet("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PaymentStatusDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPagination([FromQuery] PaymentStatusGetPaginationCommand command)
        {
            var result = await _paymentStatusService.Handle(command);
            return Ok(result);
        }

        [HttpPatch("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentStatusDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] PaymentStatusUpdateByIdCommand command)
        {
            command.Id = id;
            var result = await _paymentStatusService.Handle(command);
            return Ok(result);
        }
    }
}
