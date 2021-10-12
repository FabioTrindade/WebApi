using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Sale;
using WebApi.Ecommerce.Domain.DTOs.Sale;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Services.Services
{
    public class SaleService : ISaleService
    {
        // Dependency Injection
        private readonly ISaleRepository _saleRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly ICustomerRepository _customerRepository;

        // Constructor
        public SaleService(ISaleRepository saleRepository
            , IPaymentTypeRepository paymentTypeRepository
            , IPaymentStatusRepository paymentStatusRepository
            , IProductRepository productRepository
            , ISaleProductRepository saleProductRepository
            , ICustomerRepository customerRepository)
        {
            _saleRepository = saleRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _productRepository = productRepository;
            _saleProductRepository = saleProductRepository;
            _customerRepository = customerRepository;
        }

        // Implementations
        public async Task<GenericCommandResult> Handle(SaleCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            ValidCustomerExists(command.CustomerId);

            // Valida tipo de pagamento
            if (SaleTypeExistsAndValidateCard(command.PaymentTypeId).GetAwaiter().GetResult())
            {
                command.ValidateCreditCard();

                if (!command.IsValid)
                {
                    throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
                }
            }

            command.ValidateProduct();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            // Valida se o produto existe e tem em estoque a quantidade informada
            command.Products.ForEach(product =>
            {
                ValidProductExists(product.ProductId);

                var descriptionProduct = _productRepository.GetByIdAsync(product.ProductId).GetAwaiter().GetResult();

                if (ValidInventoryProduct(produtId: product.ProductId, quantity: product.Quantity).GetAwaiter().GetResult())
                {
                    command.AddNotification(key: "Produto", message: $"O produto '{descriptionProduct.Description.ToUpper()}' não possui estoque suficiente.");
                    throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
                }

                if (ValidAmountProduct(product.ProductId, product.Amount, product.Sale).GetAwaiter().GetResult())
                {
                    command.AddNotification(key: "Produto", message: $"O produto '{descriptionProduct.Description.ToUpper()}' esta com valor de venda/promoção inferior ao praticado.");
                    throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
                }
            });

            var paymentStatus = await _paymentStatusRepository.Get(t => t.PaymentStatusId == 1);

            var newSale = new Sale(
                                    creditCard: command.CreditCard, 
                                    verificationCode: command.VerificationCode, 
                                    validityMonth: command.ValidityMonth, 
                                    validityYear: command.ValidityYear, 
                                    creditCardName: command.CreditCardName, 
                                    paymentStatus: paymentStatus
                                );

            var saleProducts = command.Products.ConvertAll(c => new SaleProduct(
                                                                                    amount: c.Amount, 
                                                                                    quantity: c.Quantity, 
                                                                                    sale: c.Sale, 
                                                                                    saleId: newSale.Id, 
                                                                                    productId: c.ProductId
                                                                                ));

            await _saleRepository.CreateAsync(newSale);

            var customerDTO = await _customerRepository.GetWithById(newSale.Customer.Id);
            var paymentTypeDTO = await _paymentTypeRepository.GetWithById(newSale.PaymentType.Id);
            var paymentStatusDTO = await _paymentStatusRepository.GetWithById(newSale.PaymentType.Id);
            var saleProductsDTO = await _saleProductRepository.GetWithById(newSale.Id);

            var saleDTO = new SaleDTO(
                                        id: newSale.Id,
                                        createdAt: newSale.CreatedAt,
                                        updatedAt: newSale.UpdatedAt,
                                        active: newSale.Active,
                                        transaction: newSale.Transaction,
                                        customer: customerDTO,
                                        paymentType: paymentTypeDTO,
                                        paymentStatus: paymentStatusDTO,
                                        saleProducts: saleProductsDTO
                                    );

            return new GenericCommandResult(true, "", saleDTO);
        }

        public async Task<GenericCommandResult> Handle(SaleGetByIdCommand command)
        {
            var sale = await _saleRepository.GetByIdAsync(command.Id);
            sale.ValidateIfIsNull($"Não foi possível identificar a venda {command.Id}.");

            var customerDTO = await _customerRepository.GetWithById(sale.Customer.Id);
            var paymentTypeDTO = await _paymentTypeRepository.GetWithById(sale.PaymentType.Id);
            var paymentStatusDTO = await _paymentStatusRepository.GetWithById(sale.PaymentType.Id);
            var saleProductsDTO = await _saleProductRepository.GetWithById(sale.Id);

            var saleDTO = new SaleDTO(
                                        id: sale.Id, 
                                        createdAt: sale.CreatedAt, 
                                        updatedAt: sale.UpdatedAt, 
                                        active: sale.Active, 
                                        transaction: sale.Transaction, 
                                        customer: customerDTO, 
                                        paymentType: paymentTypeDTO, 
                                        paymentStatus: paymentStatusDTO, 
                                        saleProducts: saleProductsDTO
                                    );

            return new GenericCommandResult(true, "", saleDTO);
        }


        private void ValidCustomerExists(Guid customerId)
        {
            var existCustomer = _customerRepository.GetByIdAsync(customerId).GetAwaiter().GetResult();
            existCustomer.ValidateIfIsNull($"Não foi possível identificar o cliente {customerId}.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saleTypeId"></param>
        /// <returns></returns>
        private async Task<bool> SaleTypeExistsAndValidateCard(Guid saleTypeId)
        {
            var existProduct = await _paymentTypeRepository.GetByIdAsync(saleTypeId);
            existProduct.ValidateIfIsNull($"Não foi possível identificar o tipo de pagamento {saleTypeId}.");

            return existProduct.IsCreditCard;
        }

        /// <summary>
        /// Validar se o produto existe
        /// </summary>
        /// <param name="produtId"></param>
        private void ValidProductExists(Guid produtId)
        {
            var existProduct = _productRepository.GetByIdAsync(produtId).GetAwaiter().GetResult();
            existProduct.ValidateIfIsNull($"Não foi possível identificar o produto {produtId}.");
        }

        /// <summary>
        /// Valida se o produto existe e ainda possui estoque deste
        /// </summary>
        /// <param name="produtId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private async Task<bool> ValidInventoryProduct(Guid produtId, int quantity)
        {
            var existInventoryProduct = await _productRepository.GetByIdAsync(produtId);

            if (existInventoryProduct.Quantity < quantity)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> ValidAmountProduct(Guid produtId, decimal amount, decimal? sale)
        {
            var product = await _productRepository.GetByIdAsync(produtId);

            if (amount < product.Amount || sale.GetValueOrDefault() < product.Sale.GetValueOrDefault())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Atualiza o estoque do produto e caso zere inativa o mesmo
        /// </summary>
        /// <param name="produtId"></param>
        /// <param name="quantity"></param>
        private void UpdateInventory(Guid produtId, int quantity)
        {
            var product = _productRepository.GetByIdAsync(produtId).GetAwaiter().GetResult();

            var newQuantity = (product.Quantity - quantity);

            product.SetQuantity(newQuantity);

            if(product.Quantity == 0)
            {
                product.SetActive(false);
            }

            _productRepository.UpdateAsync(product).GetAwaiter().GetResult();
        }
    }
}
