using System;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.Customer
{
    public class CustomerGetByIdCommand : ICommand
    {
        public CustomerGetByIdCommand(Guid id) 
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
