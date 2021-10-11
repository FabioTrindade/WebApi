using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class PaymentType : Entity
    {
        // Constructor
        public PaymentType()
        {
        }

        public PaymentType(string description
            , bool isCreditCard)
        {
            Description = description;
            IsCreditCard = isCreditCard;
        }

        // Property
        /// <summary>
        /// Atributo utilizado para definir a descrição do tipo de venda: cartão, boleto...
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir se é necessario informar o numero do cartão no ato da venda
        /// </summary>
        public bool IsCreditCard { get; private set; }

        // Relationshoip
        public virtual ICollection<Sale> Sales { get; set; }


        // Modifier
        public void SetDescription(string description)
        {
            this.Description = description;
        }
    }
}
