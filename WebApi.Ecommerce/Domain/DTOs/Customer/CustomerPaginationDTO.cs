using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.Customer
{
    public class CustomerPaginationDTO : Pagination
    {
        public List<CustomerPaginatedDTO> Customers { get; set; } = new List<CustomerPaginatedDTO>();
    }
}
