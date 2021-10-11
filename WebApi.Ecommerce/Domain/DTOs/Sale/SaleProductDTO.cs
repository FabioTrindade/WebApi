using System;

namespace WebApi.Ecommerce.Domain.DTOs.Sale
{
    public class SaleProductDTO
    {
        /// <summary>
        /// Atributo utilizado para definir o id do produto
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o preco de venda
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Atributo utilizado para controlar o estoque
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o preco de promoção
        /// </summary>
        public decimal? Sale { get; set; }
    }
}
