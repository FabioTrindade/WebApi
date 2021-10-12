using System;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Commands.Sale
{
    public class SaleGetPaginationCommand : FilterCommand, ICommand
    {
        public Guid? ProductId { get; set; }

        public Guid? PaymentStatusId { get; set; }

        public Guid? PaymentTypeId { get; set; }

        public string Document { get; set; }
    }
}
