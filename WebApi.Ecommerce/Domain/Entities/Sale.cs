using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class Sale : Entity
    {
        // Relationship
        public Customer Customer { get; set; }

        public SaleType SaleType { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
