using System;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.PaymentStatus
{
    public class PaymentStatusPaginatedDTO : Paginated
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
        /// Atributo utilizado para definir a descrição do tipo de pagamento
        /// </summary>
        public string Description { get; private set; }
    }
}
