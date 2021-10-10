using System;

namespace WebApi.Ecommerce.Domain.DTOs.Customer
{
    public class CustomerDTO : EntityDTO
    {
        public CustomerDTO(Guid id
            , DateTime createdAt
            , DateTime? updatedAt
            , bool active
            , string name
            , string document
            , string zipCode
            , string address
            , string number
            , string neighborhood
            , string complement
            , string city
            , string state
            , string country
            , string cellPhone
            , string phone
            , string email)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Active = active;
            Name = name;
            Document = document;
            ZipCode = zipCode;
            Address = address;
            Number = number;
            Neighborhood = neighborhood;
            Complement = complement;
            City = city;
            State = state;
            Country = country;
            CellPhone = cellPhone;
            Phone = phone;
            Email = email;
        }


        // Properties
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
        public string Address { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o número da residencial
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o bairro
        /// </summary>
        public string Neighborhood { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o complemento do endereço
        /// </summary>
        public string Complement { get; set; }

        /// <summary>
        /// Atributo utilizado para definir a cidade
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o estado
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o país
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o número do celular
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o telefone fixo ou comercial
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Atributo utilizado para definir o e-mail
        /// </summary>
        public string Email { get; set; }
    }
}
