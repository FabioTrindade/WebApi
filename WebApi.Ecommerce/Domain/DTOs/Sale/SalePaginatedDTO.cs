using System;
using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.Sale
{
    public class SalePaginatedDTO : Paginated
    {
        // Properties
        /// <summary>
        /// Atributo utilizado para definir a chave primaria da venda
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
        /// Atributo utilizado para controlar alteração da transação
        /// </summary>
        public string Transaction { get; private set; }

        /// <summary>
        /// Atributo utilizado para exibir o id cliente
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Atributo utilizado para exibir o nome cliente
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Atributo utilizado para exibir o documento do cliente
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Atributo utilizado para exibir o email cliente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Atributo utilizado para exibir a descrição do tipo de pagamento
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// Atributo utilizado para exibir a descrição do status do pagamento
        /// </summary>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Atributo utilizado para exibir os produtos
        /// </summary>
        public List<SaleProductDTO> Products { get; set; } = new List<SaleProductDTO>();
    }
}
