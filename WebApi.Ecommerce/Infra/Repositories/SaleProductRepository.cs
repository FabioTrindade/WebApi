using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.DTOs.Sale;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class SaleProductRepository : EntityRepository<SaleProduct>, ISaleProductRepository
    {
        public SaleProductRepository(WebApiDataContext context
            , ILogErroRepository logErroRepository) : base(context, logErroRepository)
        {
        }

        public async Task<List<SaleProductDTO>> GetWithBySaleId(Guid id)
        {
            var sql = $@"
                            SELECT sl.productid
                                , p.description 
                                , sl.amount
                                , sl.quantity
                                , sl.sale 
                            FROM public.""SaleProducts"" sl
                                INNER JOIN ""Products"" p ON(p.id = sl.productid)
                            WHERE sl.saleid = @id
                        ";

            return await QueryAsync<SaleProductDTO>(sql, new { id });
        }
    }
}
