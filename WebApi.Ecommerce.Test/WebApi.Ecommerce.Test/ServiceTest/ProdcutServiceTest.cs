using Flunt.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Product;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.DTOs.Product;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Services.Services;

namespace WebApi.Ecommerce.Test.ServiceTest
{
    [TestClass]
    public class ProdcutServiceTest
    {
        private IProductService _productService;

        private Mock<IProductRepository> _productRepository;
        private Product product;

        [TestInitialize]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();

            _productService = new ProductService(_productRepository.Object);
            product = new Product(description: "Produto 1", sku: "PRD01", amount: 10.5M, quantity: 1, null);
        }

        [TestMethod]
        public void NotCreateProdut()
        {
            var command = new ProductCreateCommand() { Sale = 0};

            var result = Assert.ThrowsExceptionAsync<HttpException>(() => _productService.Handle(command));
            var notification = (result.Result.Result.Data as List<Notification>);

            Assert.AreEqual("Faz necessário preencher a descrição do produto.", notification[0].Message);
            Assert.AreEqual("Faz necessário preencher o identificador SKU do produto.", notification[1].Message);
            Assert.AreEqual("Faz necessário informar o valor de venda do produto.", notification[2].Message);
            Assert.AreEqual("Faz necessário informar o valor de promoção do produto.", notification[3].Message);

            command = new ProductCreateCommand()
            {
                Description = "Produto 1",
                SKU = "PRD01",
                Amount = 10.5M,
                Quantity = 1
            };

            _productRepository.Setup(s => s.Get(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(new Product()));
            result = Assert.ThrowsExceptionAsync<HttpException>(() => _productService.Handle(command));
            notification = (result.Result.Result.Data as List<Notification>);

            Assert.AreEqual("O identificador SKU encontra-se em uso.", notification[0].Message);
        }
    
        [TestMethod]
        public void CreateProduct()
        {
            var command = new ProductCreateCommand() {
                Description = "Produto 1",
                SKU = "PRD01",
                Amount = 10.5M,
                Quantity = 1
            };

            var result = _productService.Handle(command).GetAwaiter().GetResult();
            Assert.IsTrue((result as GenericCommandResult).Success);
        } 
    
        [TestMethod]
        public void NotProductGetPagination()
        {
            var command = new ProductGetPaginationCommand() { PerPage = 0 };

            var result = Assert.ThrowsExceptionAsync<HttpException>(() => _productService.Handle(command));
            var notification = (result.Result.Result.Data as List<Notification>);

            Assert.AreEqual("A quantidade de registros por página deve ser maior que zero.", notification[0].Message);
        }

        [TestMethod]
        public void ProductGetPagination()
        {
            var command = new ProductGetPaginationCommand();

            var resultBootstrapTable = new BootstrapTablePaginationDTO<ProductPaginatedDTO>()
            {
                Rows = new List<ProductPaginatedDTO>(),
                Total = 0
            };

            _productRepository.Setup(s => s.QueryPaginationAsync(It.IsAny<BootstrapTableCommand>(), It.IsAny<ProductGetPaginationCommand>())).Returns(Task.FromResult(resultBootstrapTable));

            var result = _productService.Handle(command).GetAwaiter().GetResult();
            Assert.IsTrue((result as GenericCommandResult).Success);
        }

        [TestMethod]
        public void NotUpdateProduct()
        {
            var command = new ProductUpdateByIdCommand() { Sale = 0};

            var result = Assert.ThrowsExceptionAsync<HttpException>(() => _productService.Handle(command));
            var notification = (result.Result.Result.Data as List<Notification>);

            Assert.AreEqual("Faz necessário preencher a descrição do produto.", notification[0].Message);
            Assert.AreEqual("Faz necessário preencher o identificador SKU do produto.", notification[1].Message);
            Assert.AreEqual("Faz necessário informar o valor de venda do produto.", notification[2].Message);
            Assert.AreEqual("Faz necessário informar o valor de promoção do produto.", notification[3].Message);

            command = new ProductUpdateByIdCommand()
            {
                Id = Guid.NewGuid(),
                Description = "Produto 1",
                SKU = "PRD01",
                Amount = 10.5M,
                Quantity = 1
            };

            _productRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Product()));
            _productRepository.Setup(s => s.Get(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(new Product()));

            result = Assert.ThrowsExceptionAsync<HttpException>(() => _productService.Handle(command));
            notification = (result.Result.Result.Data as List<Notification>);

            Assert.AreEqual("O identificador SKU encontra-se em uso.", notification[0].Message);
        }

        [TestMethod]
        public void UpdateProduct()
        {
            var command = new ProductUpdateByIdCommand()
            {
                Id = Guid.NewGuid(),
                Description = "Produto 1",
                SKU = "PRD01",
                Amount = 10.5M,
                Quantity = 1
            };

            _productRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Product()));
            _productRepository.Setup(s => s.Get(t => t.SKU.ToUpper() == command.SKU.ToUpper().Trim())).Returns(Task.FromResult(product));

            var result = _productService.Handle(command).GetAwaiter().GetResult();
            Assert.IsTrue((result as GenericCommandResult).Success);
        }
    
        [TestMethod]
        public void NotDeleteProduct()
        {
            var command = new ProductDeleteByIdCommand(Guid.NewGuid());
            var result = Assert.ThrowsExceptionAsync<HttpException>(() => _productService.Handle(command));

            Assert.AreEqual($"Produto {command.Id} não encontrado.", result.Result.Result.Message);
        }

        [TestMethod]
        public void DeleteProduct()
        {
            var command = new ProductDeleteByIdCommand(Guid.NewGuid());

            _productRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Product()));

            var result = _productService.Handle(command).GetAwaiter().GetResult();
            Assert.IsTrue((result as GenericCommandResult).Success);
        }
    }
}
