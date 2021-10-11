using WebApi.Ecommerce.Domain.Commands.Product;

namespace WebApi.Ecommerce.Domain.Services
{
    public interface IProductService : IService<ProductCreateCommand>
        , IService<ProductGetByIdCommand>
        , IService<ProductGetPaginationCommand>
        , IService<ProductUpdateByIdCommand>
        , IService<ProductDeleteByIdCommand>
        , IService<ProductHasInventoryGetPaginationCommand>
    {
    }
}
