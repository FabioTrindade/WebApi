using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.DTOs.PaymentType;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface IPaymentTypeRepository : IEntityRepository<PaymentType>
    {
        Task<PaymentTypeDTO> GetWithById(Guid id);
    }
}
