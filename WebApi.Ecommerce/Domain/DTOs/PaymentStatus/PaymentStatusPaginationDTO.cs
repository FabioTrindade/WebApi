using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.PaymentStatus
{
    public class PaymentStatusPaginationDTO : Pagination
    {
        public List<PaymentStatusPaginatedDTO> PaymentType { get; set; } = new List<PaymentStatusPaginatedDTO>();
    }
}
