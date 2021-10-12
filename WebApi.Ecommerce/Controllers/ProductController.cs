using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands.Product;
using WebApi.Ecommerce.Domain.DTOs.Product;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProductCreateCommand command)
        {
            var result = await _productService.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = (result.Data as ProductDTO).Id }, result.Data);
        }

        [HttpGet("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _productService.Handle(new ProductGetByIdCommand(id));
            return Ok(result);
        }

        [HttpGet("/v1/[controller]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPagination([FromQuery] ProductGetPaginationCommand command)
        {
            var result = await _productService.Handle(command);
            return Ok(result);
        }

        [HttpPatch("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] ProductUpdateByIdCommand command)
        {
            command.Id = id;
            var result = await _productService.Handle(command);
            return Ok(result);
        }

        [HttpDelete("/v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _productService.Handle(new ProductDeleteByIdCommand(id));
            return Ok();
        }
    }
}
