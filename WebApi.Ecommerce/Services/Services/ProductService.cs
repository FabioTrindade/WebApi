using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Services.Services
{
    public class ProductService : IProductService
    {
        // Dependency Injection
        private readonly IProductRepository _productRepository;

        // Constructor
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Implementations
        public async Task<GenericCommandResult> Handle(ProductCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "", command.Notifications);
            }

            if (ExistIdentifierSku(command.SKU).GetAwaiter().GetResult())
            {
                command.AddNotification("SKU", "O identificador SKU encontra-se em uso.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            var product = new Product(command.Description.Trim(), command.SKU.Trim(), command.Amount, command.Quantity, command.Sale);
            await _productRepository.CreateAsync(product);

            return new GenericCommandResult(true, "", product);
        }

        private async Task<bool> ExistIdentifierSku(string Sku)
        {
            var existSku = await _productRepository.Get(t => t.SKU == Sku.Trim());

            if(existSku is not null)
            {
                return true;
            }

            return false;
        }
    }
}
