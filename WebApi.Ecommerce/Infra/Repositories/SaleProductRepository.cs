using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class SaleProductRepository : EntityRepository<SaleProduct>, ISaleProductRepository
    {
        public SaleProductRepository(WebApiDataContext context) : base (context)
        {
        }
    }
}
