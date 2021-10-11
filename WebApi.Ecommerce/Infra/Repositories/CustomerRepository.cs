using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.DTOs.Customer;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class CustomerRepository : EntityRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(WebApiDataContext context) : base (context)
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
    }
}
