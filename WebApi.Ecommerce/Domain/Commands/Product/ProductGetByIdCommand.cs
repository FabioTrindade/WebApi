using System;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.Product
{
    public class ProductGetByIdCommand : ICommand
    {
        public ProductGetByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
