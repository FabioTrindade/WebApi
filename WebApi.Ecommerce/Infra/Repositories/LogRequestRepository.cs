using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class LogRequestRepository : EntityRepository<LogRequest>, ILogRequestRepository
    {
        public LogRequestRepository(WebApiDataContext context
            , ILogErroRepository logErroRepository) : base(context, logErroRepository)
        {
        }
    }
}
