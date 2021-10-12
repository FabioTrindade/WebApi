using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.Product
{
    public class ProductUpdateByIdCommand : Notifiable<Notification>, ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string SKU { get; set; }

        public decimal Amount { get; set; }

        public int Quantity { get; set; }

        public decimal? Sale { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                    .IsNotNullOrEmpty(Description, "Description", "Faz necessário preencher a descrição do produto.")
                    .IsNotNullOrEmpty(SKU, "SKU", "Faz necessário preencher o identificador SKU do produto.")
                    .IsGreaterThan(Amount, 0, "Amount", "Faz necessário informar o valor de venda do produto.")
                );

            if (Sale is not null)
            {
                AddNotifications(new Contract<Notification>().Requires()
                    .IsGreaterThan(Sale.GetValueOrDefault(), 0, "Sale", "Faz necessário informar o valor de promoção do produto.")
                );
            }
        }
    }
}
