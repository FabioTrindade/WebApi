using System;

namespace WebApi.Ecommerce.Domain.DTOs.PaymentStatus
{
    public class PaymentStatusDTO : EntityDTO
    {
        // Constructor
        public PaymentStatusDTO(Guid id
            , DateTime createdAt
            , DateTime? updatedAt
            , bool active
            , string description)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Active = active;
            Description = description;
        }

        // Properties
        /// <summary>
        /// Atributo utilizado para definir a descrição do tipo de pagamento
        /// </summary>
        public string Description { get; private set; }
    }
}
