using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class SaleType : Entity
    {
        // Constructor
        public SaleType()
        {
        }

        public SaleType(string description)
        {
            Description = description;
        }


        // Property
        /// <summary>
        /// Atributo utilizado para definir a descrição do tipo de venda: cartão, boleto...
        /// </summary>
        public string Description { get; private set; }


        // Relationshoip
        public virtual ICollection<Sale> Sales { get; set; }


        // Modifier
        public void SetDescription(string description)
        {
            this.Description = description;
        }
    }
}
