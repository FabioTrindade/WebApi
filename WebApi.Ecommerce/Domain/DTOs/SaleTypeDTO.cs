using System;

namespace WebApi.Ecommerce.Domain.DTOs
{
    public class SaleTypeDTO : EntityDTO
    {
        // Constructor
        public SaleTypeDTO(Guid id
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
        /// Atributo utilizado para definir a descrição do produto
        /// </summary>
        public string Description { get; set; }
    }
}
