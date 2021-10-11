using System;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class SaleProduct : Entity
    {
        // Constructor
        public SaleProduct()
        {
        }

        public SaleProduct(decimal amount
            , int quantity
            , decimal? sale
            , Guid saleId
            , Guid productId)
        {
            Amount = amount;
            Quantity = quantity;
            Sale = sale;
            SaleId = saleId;
            ProductId = productId;
        }


        // Properties
        public decimal Amount { get; private set; }

        public int Quantity { get; private set; }

        public decimal? Sale { get; set; }


        // Relationship
        public Guid SaleId { get; set; }
        public Sale Sales { get; set; }

        public Guid ProductId { get; set; }
        public Product Products { get; set; }
    }
}
