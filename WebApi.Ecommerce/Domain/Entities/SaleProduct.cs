using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class SaleProduct : Entity
    {
        // Relationship
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
