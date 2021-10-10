using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.Product
{
    public class ProductPaginationDTO : Pagination
    {
        public List<ProductPaginatedDTO> Product { get; set; } = new List<ProductPaginatedDTO>();
    }
}
