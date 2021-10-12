using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Product;
using WebApi.Ecommerce.Domain.DTOs.Product;
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
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            if (ExistIdentifierSku(command.SKU).GetAwaiter().GetResult())
            {
                command.AddNotification(key: "SKU", message: "O identificador SKU encontra-se em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var newProduct = new Product(
                                            description: command.Description.ToUpper().Trim(),
                                            sku: command.SKU.ToUpper().Trim(),
                                            amount: command.Amount, 
                                            quantity: command.Quantity, 
                                            sale: command.Sale
                                        );

            await _productRepository.CreateAsync(newProduct);

            var product = new ProductDTO(
                                            id: newProduct.Id, 
                                            createdAt: newProduct.CreatedAt, 
                                            updatedAt: newProduct.UpdatedAt, 
                                            active: newProduct.Active, 
                                            description: newProduct.Description, 
                                            sku: newProduct.SKU, 
                                            amount: newProduct.Amount, 
                                            quantity: newProduct.Quantity, 
                                            sale: newProduct.Sale
                                        );

            return new GenericCommandResult(true, "", product);
        }

        public async Task<GenericCommandResult> Handle(ProductGetByIdCommand command)
        {
            var result = await _productRepository.GetByIdAsync(command.Id);

            var product = new ProductDTO(   
                                            id: result.Id,
                                            createdAt: result.CreatedAt,
                                            updatedAt: result.UpdatedAt,
                                            active: result.Active,
                                            description: result.Description,
                                            sku: result.SKU,
                                            amount: result.Amount,
                                            quantity: result.Quantity,
                                            sale: result.Sale
                                        );

            return new GenericCommandResult(true, "", product);
        }

        public async Task<GenericCommandResult> Handle(ProductGetPaginationCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var filter = new BootstrapTableCommand()
            {
                Limit = command.PerPage,
                Offset = command.CurrentPage,
                Sort = command.OrderBy,
                Order = command.SortBy
            };

            var product = await _productRepository.QueryPaginationAsync(filter, command);

            var productPaginationDTO = new ProductPaginationDTO();
            productPaginationDTO.Products.AddRange(product.Rows);
            productPaginationDTO.PerPage = command.PerPage;
            productPaginationDTO.CurrentPage = command.CurrentPage;
            productPaginationDTO.LastPage = (product.Total / command.PerPage);
            productPaginationDTO.Total = product.Total;

            return new GenericCommandResult(true, "", productPaginationDTO);
        }

        public async Task<GenericCommandResult> Handle(ProductUpdateByIdCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var product = await _productRepository.GetByIdAsync(command.Id);
            product.ValidateIfIsNull($"Não foi possível identificar o produto {command.Id}.");

            if (ExistIdentifierSku(command.SKU).GetAwaiter().GetResult())
            {
                command.AddNotification(key: "SKU", message: "O identificador SKU encontra-se em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
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
                command.AddNotification(key: "Produto", message: "Não conseguimos identificar alteração no produto.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            return new GenericCommandResult(true, "");
        }

        public async Task<GenericCommandResult> Handle(ProductDeleteByIdCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            product.ValidateIfIsNull($"Produto {command.Id} não encontrado.");

            product.SetActive(false);
            product.SetUpdatedAt(DateTime.Now);

            await _productRepository.UpdateAsync(product);

            return new GenericCommandResult(true, "");
        }

        private async Task<bool> ExistIdentifierSku(string Sku)
        {
            var existSku = await _productRepository.Get(t => t.SKU.ToUpper() == Sku.ToUpper().Trim());

            if (existSku is not null)
            {
                return true;
            }

            return false;
        }
    }
}
