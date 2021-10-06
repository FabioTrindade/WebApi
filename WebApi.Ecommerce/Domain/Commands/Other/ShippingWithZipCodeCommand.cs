using Flunt.Br;
using Flunt.Br.Extensions;
using Flunt.Notifications;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.Other
{
    public class ShippingWithZipCodeCommand : Notifiable<Notification>, ICommand
    {
        public ShippingWithZipCodeCommand(string zipCode)
        {
            ZipCode = zipCode;
        }

        public string ZipCode { get; set; }


        public void Validate()
        {
            var contract = new Contract()
                .IsCep(ZipCode, "ZipCode", "O cep informado não parece ser válido");
        }
    }
}
