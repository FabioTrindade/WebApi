using Flunt.Notifications;
using Flunt.Validations;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Domain.Commands.Customer
{
    public class CustomerCreateCommand : Notifiable<Notification>, ICommand
    {
        /// <summary>
        /// Atributo utilizado para definir o nome do cliente
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o documento CPF ou CNPJ
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o CEP - Código de Endereçamento Postal
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o endereço do cliente
        /// </summary>
        public string Address { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir o número da residencial
        /// </summary>
        public string Number { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir o bairro
        /// </summary>
        public string Neighborhood { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir o complemento do endereço
        /// </summary>
        public string Complement { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir a cidade
        /// </summary>
        public string City { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir o estado
        /// </summary>
        public string State { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir o número do celular
        /// </summary>
        public string CellPhone { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir o telefone fixo ou comercial
        /// </summary>
        public string Phone { get;  set; }

        /// <summary>
        /// Atributo utilizado para definir o e-mail
        /// </summary>
        public string Email { get;  set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                    .IsNotNullOrEmpty(Name, "Nome Completo", "É necessário informar o nome completo.")
                    .IsGreaterThan(Name, 5, "Nome Completo", "O nome completo deve conter mais de 5 caracteres")
                    .IsNotNullOrEmpty(Address, "Endereço", "É necessário informar o endereço.")
                    .IsGreaterThan(Address, 5, "Endereço", "O endereço deve conter mais de 5 caracteres")
                    .IsNotNullOrEmpty(Number, "Número", "É necessário informar o número.")
                    .IsGreaterThan(Number, 0, "Nome Completo", "O número deve conter mais de 5 caracteres")
                    .IsNotNullOrEmpty(Neighborhood, "Bairro", "É necessário informar o bairro.")
                    .IsNotNullOrEmpty(City, "Cidade", "É necessário informar a cidade.")
                    .IsNotNullOrEmpty(State, "Estado", "É necessário informar o estado.")
                    .IsNotNullOrEmpty(CellPhone, "Celular", "É necessário informar o celular.")
                    .IsEmailOrEmpty(Email, "e-mail", "É necessário informar um e-mail.")
                    .IsFalse(Document.IsValidDocument(), "Documento", "O número de documento não é valido.")
                );
        }
    }
}
