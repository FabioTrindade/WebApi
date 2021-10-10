using System;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.Product
{
    public class ProductPaginatedDTO : Paginated
    {
        // Properties
        /// <summary>
        /// Atributo utilizado para definir a chave primaria
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Atributo utilizado para definir a data de criação
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Atributo utilizado para controlar alteração no registro
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Atributo utilizado para definir se o registro esta ativo
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Atributo utilizado para definir a descrição do produto
        /// </summary>
        public string Description { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir caracteristicas unicas afins de manter organização do estoque
        /// </summary>
        public string SKU { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir o preco de venda
        /// </summary>
        public decimal Amount { get;  set; }

        /// <summary>
        /// Atributo utilizado para controlar o estoque
        /// </summary>
        public int Quantity { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir preço do produto em promoção
        /// </summary>
        public decimal? Sale { get;  set; }
    }
}
