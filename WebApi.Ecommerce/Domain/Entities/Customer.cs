using System.Collections.Generic;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class Customer : Entity
    {
        // Constructor
        public Customer()
        {
        }

        public Customer(string name
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
        public string Name { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o documento CPF ou CNPJ
        /// </summary>
        public string Document { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o CEP - Código de Endereçamento Postal
        /// </summary>
        public string ZipCode { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o endereço do cliente
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o número da residencial
        /// </summary>
        public string Number { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o bairro
        /// </summary>
        public string Neighborhood { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o complemento do endereço
        /// </summary>
        public string Complement { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir a cidade
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o estado
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o país
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o número do celular
        /// </summary>
        public string CellPhone { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o telefone fixo ou comercial
        /// </summary>
        public string Phone { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o e-mail
        /// </summary>
        public string Email { get; private set; }


        // Relationship
        public virtual ICollection<Sale> Sales { get; set; }


        // Modifier
        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetDocument(string document)
        {
            this.Document = document;
        }

        public void SetZipCode(string zipCode)
        {
            this.ZipCode = zipCode;
        }
        
        public void SetAddress(string address)
        {
            this.Address = address;
        }
        
        public void SetNumber(string number)
        {
            this.Number = number;
        }

        public void SetNeighborhood(string neighborhood)
        {
            this.Neighborhood = neighborhood;
        }

        public void SetComplement(string complement)
        {
            this.Complement = complement;
        }

        public void SetCity(string city)
        {
            this.City = city;
        }

        public void SetState(string state)
        {
            this.State = state;
        }

        public void SetCountry(string country)
        {
            this.Country = country;
        }

        public void SetCellPhone(string cellPhone)
        {
            this.CellPhone = cellPhone;
        }

        public void SetPhone(string phone)
        {
            this.Phone = phone;
        }

        public void SetEmail(string email)
        {
            this.Email = email;
        }
    }
}
