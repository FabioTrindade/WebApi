using WebApi.Ecommerce.Domain.Commands.PaymentStatus;

namespace WebApi.Ecommerce.Domain.Services
{
    public interface IPaymentStatusService : IService<PaymentStatusCreateCommand>
        , IService<PaymentStatusGetByIdCommand>
        , IService<PaymentStatusUpdateByIdCommand>
    {
    }
}
