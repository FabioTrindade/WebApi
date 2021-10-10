using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands.SaleType;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.Product;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api")]
    public class SaleTypeController : ControllerBase
    {
        private readonly ISaleTypeService _saleTypeService;

        public SaleTypeController(ISaleTypeService saleTypeService)
        {
            _saleTypeService = saleTypeService;
        }

        [HttpPost("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaleTypeCreateCommand command)
        {
            var result = await _saleTypeService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = (result.Data as SaleTypeDTO).Id }, result.Data);
        }

        [HttpGet("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _saleTypeService.Handle(new SaleTypeGetByIdCommand(id));
            return Ok(result);
        }

        [HttpPatch("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleTypeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] SaleTypeUpdateByIdCommand command)
        {
            command.Id = id;
            var result = await _saleTypeService.Handle(command);
            return Ok(result);
        }
    }
}
