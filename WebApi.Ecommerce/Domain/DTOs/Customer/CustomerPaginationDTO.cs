using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.Customer
{
    public class CustomerPaginationDTO : Pagination
    {
        public List<CustomerPaginatedDTO> Customer { get; set; } = new List<CustomerPaginatedDTO>();
    }
}
