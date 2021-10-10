using WebApi.Ecommerce.Domain.Commands.SaleType;

namespace WebApi.Ecommerce.Domain.Services
{
    public interface ISaleTypeService : IService<SaleTypeCreateCommand>
        , IService<SaleTypeGetByIdCommand>
        , IService<SaleTypeUpdateByIdCommand>
    {
    }
}
