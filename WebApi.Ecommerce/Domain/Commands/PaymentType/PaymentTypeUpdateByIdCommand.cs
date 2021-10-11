using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.PaymentType
{
    public class PaymentTypeUpdateByIdCommand : Notifiable<Notification>, ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                    .IsNotNullOrEmpty(Description, "Descrição", "Faz necessário preencher a descrição do tipo de pagamento.")
                    .IsGreaterThan(Description, 5, "Descrição", "A descrição do tipo de pagamento deve conter mais de 5 caracteres.")
                );
        }
    }
}
