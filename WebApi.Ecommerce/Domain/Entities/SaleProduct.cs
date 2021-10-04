using System;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class SaleProduct : Entity
    {
        // Relationship
        public Guid SalesId { get; set; }
        public Sale Sales { get; set; }

        public Guid ProductId { get; set; }
        public Product Products { get; set; }
    }
}
