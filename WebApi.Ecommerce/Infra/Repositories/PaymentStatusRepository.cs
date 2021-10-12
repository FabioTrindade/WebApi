using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.PaymentStatus;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.PaymentStatus;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class PaymentStatusRepository : EntityRepository<PaymentStatus>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(WebApiDataContext context
            , ILogErroRepository logErroRepository) : base(context, logErroRepository)
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
                                WHERE ps.id = @id;
                            ";

            return await QueryFirstAsync<PaymentStatusDTO>(sql, new { id });
        }

        public async Task<BootstrapTablePaginationDTO<PaymentStatusPaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, PaymentStatusGetPaginationCommand command)
        {
            var query = $@"
                            SELECT ps.id
                                , ps.createdat
                                , ps.updatedat
                                , ps.active
                                , ps.description
	                            , count(*) over() as Total
                            FROM public.""PaymentStatus"" ps
                            WHERE 1 = 1
                          ";

            if (command.Active is not null)
            {
                query += $@"    AND ps.active = {command.Active}";
            }

            if (!string.IsNullOrEmpty(command.SearchParameter))
            {
                query += $@"
                                AND ps.description LIKE '%{command.SearchParameter}%'
                           ";
            }

            if (string.IsNullOrEmpty(filter.Order))
            {
                filter.Order = "DESC";
            }

            if (string.IsNullOrEmpty(filter.Sort))
            {
                filter.Sort = "ps.createdat";
            }

            return await QueryPaginatedAsync<PaymentStatusPaginatedDTO>(query, filter);
        }
    }
}
