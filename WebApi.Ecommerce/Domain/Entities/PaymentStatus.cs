using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class PaymentStatus : Entity
    {
        // Constructor
        public PaymentStatus()
        {
        }

        public PaymentStatus(string description
            , int paymentStatusId)
        {
            Description = description;
            PaymentStatusId = paymentStatusId;
        }

        // Property
        /// <summary>
        /// Atributo utilizado para definir a descrição do tipo de venda: cartão, boleto...
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir um id único do status do pagamento
        /// </summary>
        public int PaymentStatusId { get; set; }


        // Relationshoip
        public virtual ICollection<Sale> Sales { get; set; }


        // Modifier
        public void SetDescription(string description)
        {
            this.Description = description;
        }
    }
}
