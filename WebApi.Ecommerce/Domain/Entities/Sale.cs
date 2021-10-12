using System;
using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class Sale : Entity
    {
        public Sale()
        {
        }

        public Sale(string creditCard
            , string verificationCode
            , string validityMonth
            , string validityYear
            , string creditCardName
            , Guid customerId
            , Guid paymentTypeId
            , Guid paymentStatusId
            , List<SaleProduct> saleProducts)
        {
            CreditCard = creditCard;
            VerificationCode = verificationCode;
            ValidityMonth = validityMonth;
            ValidityYear = validityYear;
            CreditCardName = creditCardName;
            CustomerId = customerId;
            PaymentTypeId = paymentTypeId;
            PaymentStatusId = paymentStatusId;
            SaleProducts = saleProducts;
        }


        // Properties
        public string CreditCard { get; private set; }

        public string VerificationCode { get; private set; }

        public string ValidityMonth { get; private set; }

        public string ValidityYear { get; private set; }

        public string CreditCardName { get; private set; }

        public string Transaction { get; private set; }


        // Relationship
        public Guid CustomerId { get; private set; }

        public virtual Customer Customer { get; private set; }

        public Guid PaymentTypeId { get; private set; }

        public virtual PaymentType PaymentType { get; private set; }

        public Guid PaymentStatusId { get; private set; }

        public virtual PaymentStatus PaymentStatus { get; private set; }

        public IEnumerable<SaleProduct> SaleProducts { get; private set; } = new List<SaleProduct>();


        // Modifier
        public void SetTransaction(string transaction)
        {
            this.Transaction = transaction;
        }

        public void SetPaymentStatusId(Guid paymentStatusId)
        {
            this.PaymentStatusId = paymentStatusId;
        }
    }
}
