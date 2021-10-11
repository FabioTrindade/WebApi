using WebApi.Ecommerce.Domain.Commands.Sale;

namespace WebApi.Ecommerce.Domain.Services
{
    public interface ISaleService : IService<SaleCreateCommand>
        , IService<SaleGetByIdCommand>
    {
    }
}
