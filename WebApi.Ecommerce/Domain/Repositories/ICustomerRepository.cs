using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Customer;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.Customer;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Task<CustomerDTO> GetWithById(Guid id);

        Task<BootstrapTablePaginationDTO<CustomerPaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, CustomerGetPaginationCommand command);
    }
}
