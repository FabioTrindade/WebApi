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
            , PaymentStatus paymentStatus)
        {
            CreditCard = creditCard;
            VerificationCode = verificationCode;
            ValidityMonth = validityMonth;
            ValidityYear = validityYear;
            CreditCardName = creditCardName;
            PaymentStatus = paymentStatus;
        }


        // Properties
        public string CreditCard { get; private set; }

        public string VerificationCode { get; private set; }

        public string ValidityMonth { get; private set; }

        public string ValidityYear { get; private set; }

        public string CreditCardName { get; private set; }

        public string Transaction { get; private set; }


        // Relationship
        public Customer Customer { get; set; }

        public PaymentType PaymentType { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public ICollection<SaleProduct> SaleProducts { get; set; }


        // Modifier
        public void SetTransaction(string transaction)
        {
            this.Transaction = transaction;
        }
    }
}
