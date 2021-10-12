using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.DTOs.Sale;
using WebApi.Ecommerce.Domain.Entities;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface ISaleProductRepository : IEntityRepository<SaleProduct>
    {
        Task<List<SaleProductDTO>> GetWithBySaleId(Guid id);
    }
}
