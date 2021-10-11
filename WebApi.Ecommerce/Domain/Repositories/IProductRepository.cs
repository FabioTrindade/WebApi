using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Product;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.Product;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface IProductRepository : IEntityRepository<Product>
    {

        Task<BootstrapTablePaginationDTO<ProductPaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, ProductGetPaginationCommand command);

        Task<BootstrapTablePaginationDTO<ProductPaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, ProductHasInventoryGetPaginationCommand command);
    }
}
