using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using WebApi.Ecommerce.Domain.DTOs.Sale;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Domain.Commands.Sale
{
    public class SaleCreateCommand : Notifiable<Notification>, ICommand
    {
        public Guid CustomerId { get; set; }

        public Guid PaymentTypeId { get; set; }

        public string CreditCard { get; set; }

        public string VerificationCode { get; set; }

        public string ValidityMonth { get; set; }

        public string ValidityYear { get; set; }

        public string CreditCardName { get; set; }

        public List<SaleProductDTO> Products { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                    .IsNotNull(CustomerId, "CustomerId", "É necessário informar o cliente.")
                    .IsNotNull(PaymentTypeId, "PaymentTypeId", "É necessário informar o tipo de pagamento.")
                    .IsNotNull(Products, "Products", "É necessário informar o produto.")
                );
        }

        public void ValidateCreditCard()
        {
            AddNotifications(new Contract<Notification>().Requires()
                    .IsNotNullOrEmpty(CreditCard, "CreditCard", "Informe o número do cartão de crédito.")
                    .IsNotNullOrEmpty(VerificationCode, "VerificationCode", "Informe o código verificador do cartão de crédito.")
                    .IsNotNullOrEmpty(ValidityMonth, "ValidityMonth", "Informe o mês de validade do cartão.")
                    .IsNotNullOrEmpty(ValidityYear, "ValidityYear", "Informe o ano de validade do cartão.")
                    .IsNotNullOrEmpty(CreditCardName, "CreditCardName", "Informe o nome impresso no cartão de crédito.")
                    .IsGreaterThan(CreditCardName, 5, "CreditCardName", "Informe um nome com mais de 5 caracteres.")
                    .IsGreaterThan(CreditCardName, 5, "CreditCardName", "Informe um nome com mais de 5 caracteres.")
                );

            if (CreditCard is not null)
            {
                AddNotifications(new Contract<Notification>().Requires()
                        .IsCreditCard(CreditCard, "CreditCard", "Informe o número do cartão de crédito.")
                    );
            }

            if (!string.IsNullOrEmpty(ValidityYear))
            {
                var yearValidity = Convert.ToInt32(ValidityYear.OnlyNumbers());
                var monthValidity = Convert.ToInt32(ValidityMonth.OnlyNumbers());

                DateTime validityCreditCart;
                var validDate = DateTime.TryParse(string.Format("1/{0}/{1}", monthValidity, yearValidity), out validityCreditCart);

                DateTime validitycurrentDate;
                var currentDate = DateTime.TryParse(string.Format("1/{0}/{1}", DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString()), out validitycurrentDate);

                if (validityCreditCart < validitycurrentDate)
                {
                    AddNotification("ValidityCard", "O cartão informado parece estar vencido.");
                }
            }
        }

        public void ValidateProduct()
        {
            Products.ForEach(product =>
            {
                AddNotifications(new Contract<Notification>().Requires()
                        .IsGreaterThan(product.Amount, 0, "Amount", "Faltou informar o preço de venda do produto")
                        .IsGreaterThan(product.Quantity, 0, "Quantity", "Faltou informar a quantidade do produto")
                    );

                if (product.Sale is not null)
                {
                    AddNotifications(new Contract<Notification>().Requires()
                            .IsGreaterThan(product.Sale.GetValueOrDefault(), 0, "Sale", "Faltou informar o preço de promoção do produto")
                        );
                }
            });
        }
    }
}
