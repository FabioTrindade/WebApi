using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.Product
{
    public class ProductPaginationDTO : Pagination
    {
        public List<ProductPaginatedDTO> Products { get; set; } = new List<ProductPaginatedDTO>();
    }
}
