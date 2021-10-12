using System;
using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Sale;
using WebApi.Ecommerce.Domain.DTOs.Sale;
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

                // Valida a quantidade
                if (ValidInventoryProduct(produtId: product.ProductId, quantity: product.Quantity).GetAwaiter().GetResult())
                {
                    command.AddNotification(key: "Produto", message: $"O produto '{descriptionProduct.Description.ToUpper()}' não possui estoque suficiente.");
                    throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
                }

                // Valida valor de venda e promoção
                if (ValidAmountProduct(product.ProductId, product.Amount, product.Sale).GetAwaiter().GetResult())
                {
                    command.AddNotification(key: "Produto", message: $"O produto '{descriptionProduct.Description.ToUpper()}' esta com valor de venda/promoção inferior ao praticado.");
                    throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
                }
            });

            // Venda realizada via transação
            var transactionSale = await _saleRepository.TransactionSale(command);

            transactionSale.ValidateIfIsNull("Não foi possivel realizar a venda.");

            // Realiza a baixa do estoque
            command.Products.ForEach(product =>
            {
                ReduceInventory(produtId: product.ProductId, quantity: product.Quantity);
            });

            var customerDTO = await _customerRepository.GetWithById(command.CustomerId);
            var paymentTypeDTO = await _paymentTypeRepository.GetWithById(command.PaymentTypeId);
            var paymentStatusDTO = await _paymentStatusRepository.GetWithById(transactionSale.PaymentStatusId);
            var saleProductsDTO = await _saleProductRepository.GetWithBySaleId(transactionSale.Id);

            var saleDTO = new SaleDTO(
                                        id: transactionSale.Id,
                                        createdAt: transactionSale.CreatedAt,
                                        updatedAt: transactionSale.UpdatedAt,
                                        active: transactionSale.Active,
                                        transaction: transactionSale.Transaction,
                                        customer: customerDTO,
                                        paymentType: paymentTypeDTO,
                                        paymentStatus: paymentStatusDTO,
                                        saleProducts: saleProductsDTO
                                    );

            return new GenericCommandResult(true, "", saleDTO);
        }

        public async Task<GenericCommandResult> Handle(SaleGetByIdCommand command)
        {
            // Consulta a venda e valida se existe atraves do ID informado
            var sale = await _saleRepository.GetByIdAsync(command.Id);
            sale.ValidateIfIsNull($"Não foi possível identificar a venda {command.Id}.");

            var customerDTO = await _customerRepository.GetWithById(sale.CustomerId);
            var paymentTypeDTO = await _paymentTypeRepository.GetWithById(sale.PaymentTypeId);
            var paymentStatusDTO = await _paymentStatusRepository.GetWithById(sale.PaymentStatusId);
            var saleProductsDTO = await _saleProductRepository.GetWithBySaleId(sale.Id);

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

        public async Task<GenericCommandResult> Handle(SaleCancelGetByIdCommandCommand command)
        {
            // Consulta a venda e valida se existe atraves do ID informado
            var sale = await _saleRepository.GetByIdAsync(command.Id);
            sale.ValidateIfIsNull($"Não foi possível identificar a venda {command.Id}.");

            // Valida se a venda esta cancelada
            if (ProductCanceled(sale.PaymentStatusId).GetAwaiter().GetResult())
            {
                command.AddNotification(key: "Sale", message: $"A venda '{sale.Id}' já encontra-se cancelada.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            // Valida se o pedido foi enviado
            if (CanCancelSale(sale.PaymentStatusId).GetAwaiter().GetResult())
            {
                var paymentStatusNow = await _paymentStatusRepository.GetByIdAsync(sale.PaymentStatusId);
                command.AddNotification(key: "Sale", message: $"A venda '{sale.Id}' não pode ser cancelada, devido estar {paymentStatusNow.Description.ToUpper()}.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            // Captura os produtos que contempla a venda
            var products = await _saleProductRepository.GetWithBySaleId(command.Id);

            // retornar com o produto para o estoque
            products.ForEach(product => {
                IncreaseInventory(product.ProductId, product.Quantity);
            });

            // Retornar o id do status de cancelamento
            var paymentStatus = await _paymentStatusRepository.Get(t => t.PaymentStatusId == 9);

            // Atualiza o status do pagamento e salva
            sale.SetPaymentStatusId(paymentStatus.Id);
            await _saleRepository.UpdateAsync(sale);

            return new GenericCommandResult(true, "");
        }

        public async Task<GenericCommandResult> Handle(SaleGetPaginationCommand command)
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

            var sales = await _saleRepository.QueryPaginationAsync(filter, command);

            sales.Rows.ForEach(sale =>
            {
                var product = _saleProductRepository.GetWithBySaleId(sale.Id).GetAwaiter().GetResult();
                sale.Products.AddRange(product);
            });

            var salePaginationDTO = new SalePaginationDTO();
            salePaginationDTO.Sales.AddRange(sales.Rows);
            salePaginationDTO.PerPage = command.PerPage;
            salePaginationDTO.CurrentPage = command.CurrentPage;
            salePaginationDTO.LastPage = (sales.Total / command.PerPage);
            salePaginationDTO.Total = sales.Total;

            return new GenericCommandResult(true, "", salePaginationDTO);
        }

        // Help Methods
        /// <summary>
        /// Valida se o cliente existe
        /// </summary>
        /// <param name="customerId"></param>
        private void ValidCustomerExists(Guid customerId)
        {
            var existCustomer = _customerRepository.GetByIdAsync(customerId).GetAwaiter().GetResult();
            existCustomer.ValidateIfIsNull($"Não foi possível identificar o cliente {customerId}.");
        }

        /// <summary>
        /// Valida se o tipo de venda tem validação de cartão do cartao
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
            return (existInventoryProduct.Quantity < quantity);
        }

        /// <summary>
        /// Valida o valor de venda/promoção do produto
        /// </summary>
        /// <param name="produtId"></param>
        /// <param name="amount"></param>
        /// <param name="sale"></param>
        /// <returns></returns>
        private async Task<bool> ValidAmountProduct(Guid produtId, decimal amount, decimal? sale)
        {
            var product = await _productRepository.GetByIdAsync(produtId);
            return (amount < product.Amount || sale.GetValueOrDefault() < product.Sale.GetValueOrDefault());
        }

        /// <summary>
        /// Atualiza o estoque do produto quando acontece uma venda caso zere inativa o mesmo
        /// </summary>
        /// <param name="produtId"></param>
        /// <param name="quantity"></param>
        private void ReduceInventory(Guid produtId, int quantity)
        {
            var product = _productRepository.GetByIdAsync(produtId).GetAwaiter().GetResult();

            var newQuantity = (product.Quantity - quantity);

            product.SetQuantity(newQuantity);

            if (product.Quantity == 0)
            {
                product.SetActive(false);
                product.SetUpdatedAt(DateTime.Now);
            }

            _productRepository.UpdateAsync(product).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Atualiza o estoque do produto e ativa em caso de cancelamento de venda
        /// </summary>
        /// <param name="produtId"></param>
        /// <param name="quantity"></param>
        private void IncreaseInventory(Guid produtId, int quantity)
        {
            var product = _productRepository.GetByIdAsync(produtId).GetAwaiter().GetResult();

            var newQuantity = (product.Quantity + quantity);

            product.SetQuantity(newQuantity);
            product.SetActive(true);
            product.SetUpdatedAt(DateTime.Now);

            _productRepository.UpdateAsync(product).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Valida se a venda não esta cancelada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> ProductCanceled(Guid id)
        {
            var paymentStatus = await _paymentStatusRepository.GetByIdAsync(id);
            return (paymentStatus.PaymentStatusId == 9);
        }

        /// <summary>
        /// Valida se a venda pode ser cancelada : Antes de seu status ser 5 - Enviado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> CanCancelSale(Guid id)
        {
            var paymentStatus = await _paymentStatusRepository.GetByIdAsync(id);
            return (paymentStatus.PaymentStatusId > 4);
        }
    }
}
