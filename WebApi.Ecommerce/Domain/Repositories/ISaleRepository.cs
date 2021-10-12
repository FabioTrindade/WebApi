using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Sale;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.Sale;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface ISaleRepository :  IEntityRepository<Sale>
    {
        Task<Sale> TransactionSale(SaleCreateCommand command);

        Task<BootstrapTablePaginationDTO<SalePaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, SaleGetPaginationCommand command);
    }
}
