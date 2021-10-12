using System;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.DTOs.Customer
{
    public class CustomerPaginatedDTO : Paginated
    {
        // Properties
        /// <summary>
        /// Atributo utilizado para definir a chave primaria
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Atributo utilizado para definir a data de criação
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Atributo utilizado para controlar alteração no registro
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Atributo utilizado para definir se o registro esta ativo
        /// </summary>
        public bool Active { get; set; }

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
    }
}
