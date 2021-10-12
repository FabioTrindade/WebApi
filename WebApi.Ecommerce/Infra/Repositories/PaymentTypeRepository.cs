using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.PaymentType;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.PaymentType;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class PaymentTypeRepository : EntityRepository<PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(WebApiDataContext context
            , ILogErroRepository logErroRepository) : base(context, logErroRepository)
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

        public async Task<BootstrapTablePaginationDTO<PaymentTypePaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, PaymentTypeGetPaginationCommand command)
        {
            var query = $@"
                            SELECT pt.id
                                , pt.createdat
                                , pt.updatedat
                                , pt.active
                                , pt.description
                                , pt.iscreditcard
	                            , count(*) over() as Total
                            FROM public.""PaymentTypes"" pt
                            WHERE 1 = 1
                          ";

            if (command.Active is not null)
            {
                query += $@"    AND pt.active = {command.Active}";
            }

            if (!string.IsNullOrEmpty(command.SearchParameter))
            {
                query += $@"
                                AND pt.description LIKE '%{command.SearchParameter}%'
                           ";
            }

            if (string.IsNullOrEmpty(filter.Order))
            {
                filter.Order = "DESC";
            }

            if (string.IsNullOrEmpty(filter.Sort))
            {
                filter.Sort = "pt.createdat";
            }

            return await QueryPaginatedAsync<PaymentTypePaginatedDTO>(query, filter);
        }
    }
}
