using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class SaleTypeRepository : EntityRepository<SaleType>, ISaleTypeRepository
    {
        public SaleTypeRepository(WebApiDataContext context) : base (context)
        {
        }
    }
}
