using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.Sale
{
    public class SalePaginationDTO : Pagination
    {
        public List<SalePaginatedDTO> Sales { get; set; } = new List<SalePaginatedDTO>();
    }
}
