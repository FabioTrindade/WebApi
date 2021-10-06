using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Product;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

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

            var newProduct = new Product(command.Description.Trim(), command.SKU.Trim(), command.Amount, command.Quantity, command.Sale);
            await _productRepository.CreateAsync(newProduct);

            var product = new ProductDTO(newProduct.Id, newProduct.CreatedAt, newProduct.UpdatedAt, newProduct.Active, newProduct.Description, newProduct.SKU, newProduct.Amount, newProduct.Quantity, newProduct.Sale);

            return new GenericCommandResult(true, "", product);
        }

        public async Task<GenericCommandResult> Handle(ProductGetByIdCommand command)
        {
            var result = await _productRepository.GetByIdAsync(command.Id);
            var product = new ProductDTO(result.Id, result.CreatedAt, result.UpdatedAt, result.Active, result.Description, result.SKU, result.Amount, result.Quantity, result.Sale);


            return new GenericCommandResult(true, "", product);
        }

        public async Task<GenericCommandResult> Handle(ProductGetPaginationCommand command)
        {
            return new GenericCommandResult(true, "");
        }

        public async Task<GenericCommandResult> Handle(ProductUpdateByIdCommand command)
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

            var product = await _productRepository.GetByIdAsync(command.Id);

            if(product is null)
            {
                command.AddNotification("Produto", "Não foi possível identificar o produto.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            var result = product.CompareEx(command);

            if (result)
            {
                product.SetDescription(command.Description);
                product.SetSKU(command.SKU);
                product.SetAmount(command.Amount);
                product.SetQuantity(command.Quantity);
                product.SetSale(command.Sale);

                await _productRepository.UpdateAsync(product);
            }
            else
            {
                command.AddNotification("Produto", "Não conseguimos identificar alteração no produto.");
                return new GenericCommandResult(false, "", command.Notifications);
            }

            return new GenericCommandResult(true, "");
        }

        public async Task<GenericCommandResult> Handle(ProductDeleteByIdCommand command)
        {
            var result = await _productRepository.GetByIdAsync(command.Id);


            return new GenericCommandResult(true, "");
        }

        private async Task<bool> ExistIdentifierSku(string Sku)
        {
            var existSku = await _productRepository.Get(t => t.SKU == Sku.Trim());

            if (existSku is not null)
            {
                return true;
            }

            return false;
        }
    }
}
