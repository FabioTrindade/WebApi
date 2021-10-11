using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.DTOs.PaymentType;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class PaymentTypeRepository : EntityRepository<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(WebApiDataContext context) : base(context)
        {
        }

        public async Task<PaymentTypeDTO> GetWithById(Guid id)
        {
            var sql = $@"
                                SELECT pt.id
                                    , pt.createdat
                                    , pt.updatedat
                                    , pt.active
                                    , pt.description
                                    , pt.iscreditcard
                                FROM public.""PaymentTypes"" pt
                                WHERE pt.id = @id;
                            ";

            return await QueryFirstAsync<PaymentTypeDTO>(sql, new { id });
        }
    }
}
