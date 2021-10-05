using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class SaleRepository : EntityRepository<Sale>, ISaleRepository
    {
        public SaleRepository(WebApiDataContext context) : base (context)
        {
        }
    }
}
