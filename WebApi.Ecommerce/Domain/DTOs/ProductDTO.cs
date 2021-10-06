using System;

namespace WebApi.Ecommerce.Domain.DTOs
{
    public class ProductDTO : EntityDTO
    {
        public ProductDTO( Guid id
            , DateTime createdAt
            , DateTime? updatedAt
            , bool active
            , string description
            , string sku
            , decimal amount
            , int quantity
            , decimal? sale)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Active = active;
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
    }
}
