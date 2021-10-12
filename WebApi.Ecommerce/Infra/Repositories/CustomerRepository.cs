using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Customer;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.Customer;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class CustomerRepository : EntityRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(WebApiDataContext context
            , ILogErroRepository logErroRepository) : base (context, logErroRepository)
        {
        }

        public async Task<CustomerDTO> GetWithById(Guid id)
        {
            var sql = $@"
                                SELECT c.id
                                    , c.createdat
                                    , c.updatedat
                                    , c.active
                                    , c.name
                                    , c.document
                                    , c.zipcode
                                    , c.address
                                    , c.number
                                    , c.neighborhood
                                    , c.complement
                                    , c.city
                                    , c.state
                                    , c.country
                                    , c.cellphone
                                    , c.phone
                                    , c.email
                                FROM public.""Customers"" c
                                WHERE c.id = @id;
                            ";

            return await QueryFirstAsync<CustomerDTO>(sql, new { id });
        }

        public async Task<BootstrapTablePaginationDTO<CustomerPaginatedDTO>> QueryPaginationAsync(BootstrapTableCommand filter, CustomerGetPaginationCommand command)
        {
            var query = $@"
                            SELECT c.id
                                , c.name
                                , c.document
                                , c.zipcode
                                , c.address
                                , c.number
                                , c.neighborhood
                                , c.complement
                                , c.city
                                , c.state
                                , c.country
                                , c.cellphone
                                , c.phone
                                , c.email
                                , c.createdat
                                , c.updatedat
                                , c.active
	                            , count(*) over() as Total
                            FROM public.""Customers"" c
                            WHERE 1 = 1
                          ";

            if (command.Active is not null)
            {
                query += $@"    AND c.active = {command.Active}";
            }

            if (!string.IsNullOrEmpty(command.SearchParameter))
            {
                query += $@"
                                AND (
		                            c.name LIKE '%{command.SearchParameter}%'
		                            or c.document LIKE '%{command.SearchParameter}%'
		                            or c.zipcode LIKE '%{command.SearchParameter}%'
		                            or c.address LIKE '%{command.SearchParameter}%'
		                            or c.neighborhood LIKE '%{command.SearchParameter}%'
                                    or c.city LIKE '%{command.SearchParameter}%'
                                    or c.state LIKE '%{command.SearchParameter}%'
                                    or c.country LIKE '%{command.SearchParameter}%'
                                    or c.cellphone LIKE '%{command.SearchParameter}%'
                                    or c.phone LIKE '%{command.SearchParameter}%'
                                    or c.email LIKE '%{command.SearchParameter}%'
	                            )
                           ";
            }

            if (string.IsNullOrEmpty(filter.Order))
            {
                filter.Order = "DESC";
            }

            if (string.IsNullOrEmpty(filter.Sort))
            {
                filter.Sort = "c.createdat";
            }

            return await QueryPaginatedAsync<CustomerPaginatedDTO>(query, filter);
        }
    }
}
