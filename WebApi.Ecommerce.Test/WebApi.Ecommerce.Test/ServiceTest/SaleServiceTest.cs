using Flunt.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands.Sale;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Services.Services;

namespace WebApi.Ecommerce.Test.ServiceTest
{
    [TestClass]
    public class SaleServiceTest
    {
        private ISaleService _saleService;

        private Mock<ISaleRepository> _saleRepository;
        private Mock<IPaymentTypeRepository> _paymentTypeRepository;
        private Mock<IPaymentStatusRepository> _paymentStatusRepository;
        private Mock<IProductRepository> _productRepository;
        private Mock<ISaleProductRepository> _saleProductRepository;
        private Mock<ICustomerRepository> _customerRepository;

        [TestInitialize]
        public void Setup()
        {
            _saleRepository = new Mock<ISaleRepository>();
            _saleRepository = new Mock<ISaleRepository>();
            _paymentTypeRepository = new Mock<IPaymentTypeRepository>();
            _paymentStatusRepository = new Mock<IPaymentStatusRepository>();
            _productRepository = new Mock<IProductRepository>();
            _saleProductRepository = new Mock<ISaleProductRepository>();
            _customerRepository = new Mock<ICustomerRepository>();

            _saleService = new SaleService(_saleRepository.Object
                , _paymentTypeRepository.Object
                , _paymentStatusRepository.Object
                , _productRepository.Object
                , _saleProductRepository.Object
                , _customerRepository.Object);
        }

        [TestMethod]
        public void NotCreateSale()
        {
            var command = new SaleCreateCommand();

            var result = Assert.ThrowsExceptionAsync<HttpException>(() => _saleService.Handle(command));
            var notification = (result.Result.Result.Data as List<Notification>);

            Assert.AreEqual("É necessário informar o cliente..", notification[0].Message);
            Assert.AreEqual("É necessário informar o tipo de pagamento.", notification[1].Message);
            Assert.AreEqual("É necessário informar o produto.", notification[2].Message);
        }
    }
}
