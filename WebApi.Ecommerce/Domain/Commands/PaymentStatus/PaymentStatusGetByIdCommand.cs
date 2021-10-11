using System;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.PaymentStatus
{
    public class PaymentStatusGetByIdCommand : ICommand
    {
        public PaymentStatusGetByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
