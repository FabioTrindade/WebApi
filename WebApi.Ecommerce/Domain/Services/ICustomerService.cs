using WebApi.Ecommerce.Domain.Commands.Customer;

namespace WebApi.Ecommerce.Domain.Services
{
    public interface ICustomerService : IService<CustomerCreateCommand>
        , IService<CustomerGetByIdCommand>
        , IService<CustomerGetPaginationCommand>
    {
    }
}
