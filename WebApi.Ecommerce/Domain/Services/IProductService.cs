using WebApi.Ecommerce.Domain.Commands;

namespace WebApi.Ecommerce.Domain.Services
{
    public interface IProductService : IService<ProductCreateCommand>
    {
    }
}
