using Flunt.Notifications;
using Flunt.Validations;
using System;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.Sale
{
    public class SaleCreateCommand : Notifiable<Notification>, ICommand
    {
        public Guid CustomerId { get; set; }

        public Guid SaleTypeId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                    //.IsNotNullOrEmpty(Description, "Descrição", "É necessário informar a descrição do tipo de pagamento.")
                    //.IsGreaterThan(Description, 5, "Descrição", "A descrição do tipo de pagamento deve conter mais que 5 caracteres.")
                );
        }
    }
}
