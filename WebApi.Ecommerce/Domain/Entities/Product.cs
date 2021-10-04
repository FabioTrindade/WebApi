using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class Product : Entity
    {
        // Constructor
        public Product()
        {

        }

        public Product(string description
            , string sku
            , decimal amount
            , int quantity
            , decimal? sale)
        {
            Description = description;
            SKU = sku;
            Amount = amount;
            Quantity = quantity;
            Sale = sale;
        }


        // Properties
        /// <summary>
        /// Atributo utilizado para definir a descrição do produto
        /// </summary>
        public string Description { get; private set; }
        
        /// <summary>
        /// Atributo utilizado para definir caracteristicas unicas afins de manter organização do estoque
        /// </summary>
        public string SKU { get; private set; }
        
        /// <summary>
        /// Atributo utilizado para definir o preco de venda
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Atributo utilizado para controlar o estoque
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir preço do produto em promoção
        /// </summary>
        public decimal? Sale { get; private set; }


        // Relationship
        public ICollection<SaleProduct> SaleProducts { get; set; }


        // Modifier
        public void SetDescription(string description)
        {
            this.Description = description;
        }

        public void SetSKU(string sku)
        {
            this.SKU = sku;
        }

        public void SetAmount(decimal amount)
        {
            this.Amount = amount;
        }

        public void SetQuantity(int quantity)
        {
            this.Quantity = quantity;
        }

        public void SetSale(decimal Sale)
        {
            this.Sale = Sale;
        }
    }
}
