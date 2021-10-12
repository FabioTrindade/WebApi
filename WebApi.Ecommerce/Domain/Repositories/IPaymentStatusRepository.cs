using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.PaymentStatus;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.PaymentStatus;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface IPaymentStatusRepository : IEntityRepository<PaymentStatus>
    {
        Task<PaymentStatusDTO> GetWithById(Guid id);

        Task<BootstrapTablePaginationDTO<PaymentStatusPaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, PaymentStatusGetPaginationCommand command);
    }
}
