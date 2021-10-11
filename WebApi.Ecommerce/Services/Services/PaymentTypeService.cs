using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.PaymentType;
using WebApi.Ecommerce.Domain.DTOs.PaymentType;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Services.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        // Dependency Injection
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        // Constructor
        public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        // Implementations
        public async Task<GenericCommandResult> Handle(PaymentTypeCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "", command.Notifications);
            }

            if (ExistPaymentType(command.Description).GetAwaiter().GetResult())
            {
                command.AddNotification("Descrição", $"A descrição '{command.Description}' consta em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var newSaleType = new PaymentType(command.Description.Trim(), command.IsCreditCard);
            await _paymentTypeRepository.CreateAsync(newSaleType);

            var paymentType = new PaymentTypeDTO(newSaleType.Id, newSaleType.CreatedAt, newSaleType.UpdatedAt, newSaleType.Active, newSaleType.Description, newSaleType.IsCreditCard);

            return new GenericCommandResult(true, "", paymentType);
        }

        public async Task<GenericCommandResult> Handle(PaymentTypeGetByIdCommand command)
        {
            var result = await _paymentTypeRepository.GetByIdAsync(command.Id);

            result.ValidateIfIsNull($"Não foi possível identificar o tipo de pagamento {command.Id}.");

            var paymentType = new PaymentTypeDTO(result.Id, result.CreatedAt, result.UpdatedAt, result.Active, result.Description, result.IsCreditCard);

            return new GenericCommandResult(true, "", paymentType);
        }

        public async Task<GenericCommandResult> Handle(PaymentTypeUpdateByIdCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var paymentType = await _paymentTypeRepository.GetByIdAsync(command.Id);
            paymentType.ValidateIfIsNull($"Não foi possível identificar o tipo de pagamento {command.Id}.");

            if (ExistPaymentType(command.Description).GetAwaiter().GetResult())
            {
                command.AddNotification("Tipo Pagamento", "O tipo de pagamento encontra-se em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var result = paymentType.CompareEx(command);

            if (result)
            {
                paymentType.SetDescription(command.Description);

                await _paymentTypeRepository.UpdateAsync(paymentType);
            }
            else
            {
                command.AddNotification("Tipo Pagamento", "Não conseguimos identificar alteração no tipo de pagamento.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var paymentTypeDTO = new PaymentTypeDTO(paymentType.Id, paymentType.CreatedAt, paymentType.UpdatedAt, paymentType.Active, paymentType.Description, paymentType.IsCreditCard);

            return new GenericCommandResult(true, "", paymentTypeDTO);
        }

        private async Task<bool> ExistPaymentType(string description)
        {
            var existPaymentType = await _paymentTypeRepository.Get(t => t.Description == description.Trim());

            if (existPaymentType is not null)
            {
                return true;
            }

            return false;
        }
    }
}
