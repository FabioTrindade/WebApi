using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands.Sale;
using WebApi.Ecommerce.Domain.DTOs.Sale;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaleCreateCommand command)
        {
            var result = await _saleService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = (result.Data as SaleDTO).Id }, result.Data);
        }

        [HttpGet("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleDTO))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _saleService.Handle(new SaleGetByIdCommand(id));
            return Ok(result);
        }

        [HttpGet("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaleDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPagination([FromQuery] SaleGetPaginationCommand command)
        {
            var result = await _saleService.Handle(command);
            return Ok(result);
        }

        [HttpPatch("/v1/[controller]/cancel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelGetById([FromRoute] Guid id)
        {
            var result = await _saleService.Handle(new SaleCancelGetByIdCommandCommand(id));
            return Ok(result);
        }
    
    }
}
