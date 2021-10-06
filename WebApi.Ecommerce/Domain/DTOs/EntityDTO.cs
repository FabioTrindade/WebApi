using System;

namespace WebApi.Ecommerce.Domain.DTOs
{
    public class EntityDTO
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
    }
}
