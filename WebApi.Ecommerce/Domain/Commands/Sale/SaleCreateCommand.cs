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

        public Guid SaleTypeId { get; set; }

        public string CreditCard { get; set; }

        public string VerificationCode { get; set; }

        public string ValidityMonth { get; set; }

        public string ValidityYear { get; set; }

        public string CreditCardName { get; set; }

        public List<SaleProductDTO> Products { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                    .IsNotNull(CustomerId, "Cliente", "É necessário informar o cliente.")
                    .IsNotNull(SaleTypeId, "Tipo de Pagamento", "É necessário informar o tipo de pagamento.")
                    .IsNotNull(Products, "Produto", "É necessário informar o produto.")
                );
        }

        public void ValidateCreditCard()
        {
            if (!string.IsNullOrEmpty(CreditCard))
            {
                AddNotifications(new Contract<Notification>().Requires()
                        .IsNotNullOrEmpty(CreditCard, "Número Cartão", "Informe o número do cartão de crédito.")
                        .IsNotNullOrEmpty(VerificationCode, "Código Verificador", "Informe o código verificador do cartão de crédito.")
                        .IsNotNullOrEmpty(ValidityMonth, "Mês Validade", "Informe o mês de validade do cartão.")
                        .IsNotNullOrEmpty(ValidityYear, "Ano Validade", "Informe o ano de validade do cartão.")
                        .IsNotNullOrEmpty(CreditCardName, "Nome Impresso Cartão", "Informe o nome impresso no cartão de crédito.")
                        .IsGreaterThan(CreditCardName, 5, "Nome Impresso Cartão", "Informe um nome com mais de 5 caracteres.")
                        .IsGreaterThan(CreditCardName, 5, "Nome Impresso Cartão", "Informe um nome com mais de 5 caracteres.")
                    );

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
                        AddNotification("Validade Cartão", "O cartão informado parece estar vencido.");
                    }
                }
            }
        }
    }
}
