using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class LogErroRepository : EntityRepository<LogErro>, ILogErroRepository
    {
        public LogErroRepository(WebApiDataContext context) : base(context)
        {
        }
    }
}
