using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.DTOs.Customer;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Task<CustomerDTO> GetWithById(Guid id);
    }
}
