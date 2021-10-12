using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Product;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.Product;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class ProductRepository : EntityRepository<Product>, IProductRepository
    {
        public ProductRepository(WebApiDataContext context
            , ILogErroRepository logErroRepository) : base(context, logErroRepository)
        {
        }

        public async Task<BootstrapTablePaginationDTO<ProductPaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, ProductGetPaginationCommand command)
        {
            var query = $@"
                            SELECT p.id
	                            , p.description
	                            , p.sku
	                            , p.amount
	                            , p.quantity
	                            , p.sale
	                            , p.createdat
	                            , p.updatedat
	                            , p.active
	                            , count(*) over() as Total
                            FROM public.""Products"" p
                            WHERE 1 = 1
                          ";

            if (command.Active is not null)
            {
                query += $@"    AND p.active = {command.Active}";
            }

            if(!string.IsNullOrEmpty(command.SearchParameter))
            {
                query += $@"
                                AND (
		                            p.description LIKE '%{command.SearchParameter}%'
		                            or p.sku LIKE '%{command.SearchParameter}%'
		                            or p.amount LIKE '%{command.SearchParameter}%'
		                            or p.quantity LIKE '%{command.SearchParameter}%'
		                            or p.sale LIKE '%{command.SearchParameter}%'
	                            )
                           ";
            }

            if (string.IsNullOrEmpty(filter.Order))
            {
                filter.Order = "DESC";
            }

            if (string.IsNullOrEmpty(filter.Sort))
            {
                filter.Sort = "p.createdat";
            }

            return await QueryPaginatedAsync<ProductPaginatedDTO>(query, filter);
        }
    }
}
