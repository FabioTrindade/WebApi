using System;
using System.Collections.Generic;
using WebApi.Ecommerce.Domain.DTOs.Customer;
using WebApi.Ecommerce.Domain.DTOs.PaymentStatus;
using WebApi.Ecommerce.Domain.DTOs.PaymentType;

namespace WebApi.Ecommerce.Domain.DTOs.Sale
{
    public class SaleDTO : EntityDTO
    {
        public SaleDTO(
            Guid id
            , DateTime createdAt
            , DateTime? updatedAt
            , bool active
            , string transaction
            , CustomerDTO customer
            , PaymentTypeDTO paymentType
            , PaymentStatusDTO paymentStatus
            , List<SaleProductDTO> saleProducts)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Active = active;
            Transaction = transaction;
            Customer = customer;
            PaymentType = paymentType;
            PaymentStatus = paymentStatus;
            SaleProducts = saleProducts;
        }


        public string Transaction { get; private set; }

        public CustomerDTO Customer { get; set; }

        public PaymentTypeDTO PaymentType { get; set; }

        public PaymentStatusDTO PaymentStatus { get; set; }

        public List<SaleProductDTO> SaleProducts { get; set; } = new List<SaleProductDTO>();
    }
}
