using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Ecommerce.Domain.Abstracts
{
    public abstract class Entity
    {
        // Constructor
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            Active = true;
        }

        // Properties
        /// <summary>
        /// Atributo utilizado para definir a chave primaria
        /// </summary>
        [Key]
        public Guid Id { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir a data de criação
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Atributo utilizado para controlar alteração no registro
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir se o registro esta ativo
        /// </summary>
        public bool Active { get; private set; }

        // Modifier
        public void SetUpdatedAt(DateTime? updatedAt)
        {
            this.UpdatedAt = updatedAt;
        }

        public void SetActive(bool active)
        {
            this.Active = active;
        }
    }
}
