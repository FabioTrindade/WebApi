using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.PaymentType
{
    public class PaymentTypePaginationDTO : Pagination
    {
        public List<PaymentTypePaginatedDTO> PaymentTypes { get; set; } = new List<PaymentTypePaginatedDTO>();
    }
}
