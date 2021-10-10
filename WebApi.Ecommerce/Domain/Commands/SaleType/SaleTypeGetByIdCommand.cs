using System;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.SaleType
{
    public class SaleTypeGetByIdCommand : ICommand
    {
        public SaleTypeGetByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
