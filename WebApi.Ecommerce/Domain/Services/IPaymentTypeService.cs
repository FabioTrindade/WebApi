using WebApi.Ecommerce.Domain.Commands.PaymentType;

namespace WebApi.Ecommerce.Domain.Services
{
    public interface IPaymentTypeService : IService<PaymentTypeCreateCommand>
        , IService<PaymentTypeGetByIdCommand>
        , IService<PaymentTypeUpdateByIdCommand>
    {
    }
}
