using Flunt.Notifications;
using System;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.Sale
{
    public class SaleCancelGetByIdCommandCommand : Notifiable<Notification>, ICommand
    {
        public SaleCancelGetByIdCommandCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
