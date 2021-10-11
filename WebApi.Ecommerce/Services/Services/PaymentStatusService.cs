using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.PaymentStatus;
using WebApi.Ecommerce.Domain.DTOs.PaymentStatus;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Services.Services
{
    public class PaymentStatusService : IPaymentStatusService
    {
        // Dependency Injection
        private readonly IPaymentStatusRepository _paymentStatusRepository;

        // Constructor
        public PaymentStatusService(IPaymentStatusRepository paymentStatusRepository)
        {
            _paymentStatusRepository = paymentStatusRepository;
        }

        // Implementations
        public async Task<GenericCommandResult> Handle(PaymentStatusCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "", command.Notifications);
            }

            if (ExistPaymentStatus(command.Description).GetAwaiter().GetResult())
            {
                command.AddNotification("Descrição", $"A descrição '{command.Description}' consta em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            if (ExistPaymentStatusId(command.PaymentStatusId).GetAwaiter().GetResult())
            {
                command.AddNotification("Status Id", $"O status id '{command.PaymentStatusId}' consta em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var newPaymentStatus = new PaymentStatus(command.Description.Trim(), command.PaymentStatusId);
            await _paymentStatusRepository.CreateAsync(newPaymentStatus);

            var paymentStatus = new PaymentStatusDTO(newPaymentStatus.Id, newPaymentStatus.CreatedAt, newPaymentStatus.UpdatedAt, newPaymentStatus.Active, newPaymentStatus.Description);

            return new GenericCommandResult(true, "", paymentStatus);
        }

        public async Task<GenericCommandResult> Handle(PaymentStatusGetByIdCommand command)
        {
            var result = await _paymentStatusRepository.GetByIdAsync(command.Id);

            result.ValidateIfIsNull($"Não foi possível identificar o status de pagamento {command.Id}.");

            var paymentStatus = new PaymentStatusDTO(result.Id, result.CreatedAt, result.UpdatedAt, result.Active, result.Description);

            return new GenericCommandResult(true, "", paymentStatus);
        }

        public async Task<GenericCommandResult> Handle(PaymentStatusUpdateByIdCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var saleType = await _paymentStatusRepository.GetByIdAsync(command.Id);
            saleType.ValidateIfIsNull($"Não foi possível identificar o status de pagamento {command.Id}.");

            if (ExistPaymentStatus(command.Description).GetAwaiter().GetResult())
            {
                command.AddNotification("Tipo Pagamento", "O status de pagamento encontra-se em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var result = saleType.CompareEx(command);

            if (result)
            {
                saleType.SetDescription(command.Description);

                await _paymentStatusRepository.UpdateAsync(saleType);
            }
            else
            {
                command.AddNotification("Tipo Pagamento", "Não conseguimos identificar alteração no status de pagamento.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var paymentStatusDTO = new PaymentStatusDTO(saleType.Id, saleType.CreatedAt, saleType.UpdatedAt, saleType.Active, saleType.Description);

            return new GenericCommandResult(true, "", paymentStatusDTO);
        }

        private async Task<bool> ExistPaymentStatus(string description)
        {
            var existPaymentStatus = await _paymentStatusRepository.Get(t => t.Description == description.Trim());

            if (existPaymentStatus is not null)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ExistPaymentStatusId(int paymentStatusId)
        {
            var existPaymentStatus = await _paymentStatusRepository.Get(t => t.PaymentStatusId == paymentStatusId);

            if (existPaymentStatus is not null)
            {
                return true;
            }

            return false;
        }
    }
}
