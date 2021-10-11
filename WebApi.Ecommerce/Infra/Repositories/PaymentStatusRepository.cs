using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.DTOs.PaymentStatus;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class PaymentStatusRepository : EntityRepository<PaymentStatus>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(WebApiDataContext context) : base(context)
        {
        }

        public async Task<PaymentStatusDTO> GetWithById(Guid id)
        {
            var sql = $@"
                                SELECT ps.id
                                    , ps.createdat
                                    , ps.updatedat
                                    , ps.active
                                    , ps.description
                                FROM public.""PaymentStatus"" ps
                                WHERR ps.id = @id;
                            ";

            return await QueryFirstAsync<PaymentStatusDTO>(sql, new { id });
        }
    }
}
