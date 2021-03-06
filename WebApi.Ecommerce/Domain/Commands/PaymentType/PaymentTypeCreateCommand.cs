using Flunt.Notifications;
using Flunt.Validations;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.PaymentType
{
    public class PaymentTypeCreateCommand : Notifiable<Notification>, ICommand
    {
        // Properties
        public string Description { get; set; }

        public bool IsCreditCard { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                    .IsNotNullOrEmpty(Description, "Description", "É necessário informar a descrição do tipo de pagamento.")
                    .IsGreaterThan(Description, 5, "Description", "A descrição do tipo de pagamento deve conter mais que 5 caracteres.")
                    .IsNotNull(IsCreditCard, "IsCreditCard", "Faltou informar se é um tipo de pagamento via cartão.")
                );
        }
    }
}
