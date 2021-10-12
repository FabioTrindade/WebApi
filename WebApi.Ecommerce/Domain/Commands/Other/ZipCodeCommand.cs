using Flunt.Br;
using Flunt.Br.Extensions;
using Flunt.Notifications;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Domain.Commands.Other
{
    public class ZipCodeCommand : Notifiable<Notification>, ICommand
    {
        public ZipCodeCommand(string zipCode)
        {
            ZipCode = zipCode;
        }

        public string ZipCode { get; private set; }


        public void Validate()
        {
            var contract = new Contract()
                .IsCep(ZipCode, "ZipCode", "O cep informado parece não ser válido");

            ZipCode = ZipCode.OnlyNumbers();
        }
    }
}
