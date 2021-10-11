using System;

namespace WebApi.Ecommerce.Domain.DTOs.PaymentType
{
    public class PaymentTypeDTO : EntityDTO
    {
        // Constructor
        public PaymentTypeDTO(Guid id
            , DateTime createdAt
            , DateTime? updatedAt
            , bool active
            , string description
            , bool isCreditCard)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Active = active;
            Description = description;
            IsCreditCard = isCreditCard;
        }

        // Properties
        /// <summary>
        /// Atributo utilizado para definir a descrição do tipo de pagamento
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir se o tipo de pagamento é cartão
        /// </summary>
        public bool IsCreditCard { get; private set; }
    }
}
