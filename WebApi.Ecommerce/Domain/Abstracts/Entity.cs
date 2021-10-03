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
        [Key]
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
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
