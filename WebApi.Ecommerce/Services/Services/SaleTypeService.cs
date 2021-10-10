using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.SaleType;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Services.Services
{
    public class SaleTypeService : ISaleTypeService
    {
        // Dependency Injection
        private readonly ISaleTypeRepository _saleTypeRepository;

        // Constructor
        public SaleTypeService(ISaleTypeRepository saleTypeRepository)
        {
            _saleTypeRepository = saleTypeRepository;
        }

        // Implementations
        public async Task<GenericCommandResult> Handle(SaleTypeCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "", command.Notifications);
            }

            if (ExistSaleType(command.Description).GetAwaiter().GetResult())
            {
                command.AddNotification("Descrição", $"A descrição '{command.Description}' consta em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var newSaleType = new SaleType(command.Description.Trim());
            await _saleTypeRepository.CreateAsync(newSaleType);

            var saleType = new SaleTypeDTO(newSaleType.Id, newSaleType.CreatedAt, newSaleType.UpdatedAt, newSaleType.Active, newSaleType.Description);

            return new GenericCommandResult(true, "", saleType);
        }

        public async Task<GenericCommandResult> Handle(SaleTypeGetByIdCommand command)
        {
            var result = await _saleTypeRepository.GetByIdAsync(command.Id);
            var saleType = new SaleTypeDTO(result.Id, result.CreatedAt, result.UpdatedAt, result.Active, result.Description);


            return new GenericCommandResult(true, "", saleType);
        }

        public async Task<GenericCommandResult> Handle(SaleTypeUpdateByIdCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var saleType = await _saleTypeRepository.GetByIdAsync(command.Id);
            saleType.ValidateIfIsNull($"Não foi possível identificar o tipo de pagamento {command.Id}.");

            if (ExistSaleType(command.Description).GetAwaiter().GetResult())
            {
                command.AddNotification("Tipo Pagamento", "O tipo de pagamento encontra-se em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var result = saleType.CompareEx(command);

            if (result)
            {
                saleType.SetDescription(command.Description);

                await _saleTypeRepository.UpdateAsync(saleType);
            }
            else
            {
                command.AddNotification("Tipo Pagamento", "Não conseguimos identificar alteração no tipo de pagamento.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var saleTypeDTO = new SaleTypeDTO(saleType.Id, saleType.CreatedAt, saleType.UpdatedAt, saleType.Active, saleType.Description);

            return new GenericCommandResult(true, "", saleTypeDTO);
        }

        private async Task<bool> ExistSaleType(string description)
        {
            var existSaleType = await _saleTypeRepository.Get(t => t.Description == description.Trim());

            if (existSaleType is not null)
            {
                return true;
            }

            return false;
        }
    }
}
