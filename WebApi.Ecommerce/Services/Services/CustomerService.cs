using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Customer;
using WebApi.Ecommerce.Domain.Commands.Other;
using WebApi.Ecommerce.Domain.DTOs.Customer;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Providers;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Services;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Services.Services
{
    public class CustomerService : ICustomerService
    {
        // Dependency Injection
        private readonly ICustomerRepository _customerRepository;
        private readonly IZipCodeProvider _zipCodeProvider;

        // Constructor
        public CustomerService(ICustomerRepository customerRepository
            , IZipCodeProvider zipCodeProvider)
        {
            _customerRepository = customerRepository;
            _zipCodeProvider = zipCodeProvider;
        }

        // Implementations
        public async Task<GenericCommandResult> Handle(CustomerCreateCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            // Valida se o cliente consta na base atraves do documento e e-mail
            if (ExistCustomer(command.Document, command.Email).GetAwaiter().GetResult())
            {
                command.AddNotification(key: "Cliente", message: "O cliente informado já consta em uso.");
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            // Valida se o CEP informado existe
            var zipCode = await _zipCodeProvider.Handle(new ZipCodeCommand(command.ZipCode));

            var newCustomer = new Customer(
                                            name: command.Name,
                                            document: command.Document.OnlyNumbers(),
                                            zipCode: command.ZipCode.OnlyNumbers(),
                                            address: command.Address,
                                            number: command.Number,
                                            neighborhood: command.Neighborhood,
                                            complement: command.Complement,
                                            city: command.City,
                                            state: command.State,
                                            cellPhone: command.CellPhone.OnlyNumbers(),
                                            phone: command.Phone.OnlyNumbers(),
                                            email: command.Email
                                        );

            await _customerRepository.CreateAsync(newCustomer);

            var customer = new CustomerDTO(
                                            id: newCustomer.Id,
                                            createdAt: newCustomer.CreatedAt,
                                            updatedAt: newCustomer.UpdatedAt,
                                            active: newCustomer.Active,
                                            name: newCustomer.Name,
                                            document: newCustomer.Document,
                                            zipCode: newCustomer.ZipCode,
                                            address: newCustomer.Address,
                                            number: newCustomer.Number,
                                            neighborhood: newCustomer.Neighborhood,
                                            complement: newCustomer.Complement,
                                            city: newCustomer.City,
                                            state: newCustomer.State,
                                            country: newCustomer.Country,
                                            cellPhone: newCustomer.CellPhone,
                                            phone: newCustomer.Phone,
                                            email: newCustomer.Email
                                        );

            return new GenericCommandResult(true, "", customer);
        }

        public async Task<GenericCommandResult> Handle(CustomerGetByIdCommand command)
        {
            var result = await _customerRepository.GetByIdAsync(command.Id);
            var customer = new CustomerDTO(
                                            id: result.Id,
                                            createdAt: result.CreatedAt,
                                            updatedAt: result.UpdatedAt,
                                            active: result.Active,
                                            name: result.Name,
                                            document: result.Document,
                                            zipCode: result.ZipCode,
                                            address: result.Address,
                                            number: result.Number,
                                            neighborhood: result.Neighborhood,
                                            complement: result.Complement,
                                            city: result.City,
                                            state: result.State,
                                            country: result.Country,
                                            cellPhone: result.CellPhone,
                                            phone: result.Phone,
                                            email: result.Email
                                        );

            return new GenericCommandResult(true, "", customer);
        }

        public async Task<GenericCommandResult> Handle(CustomerGetPaginationCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new GenericCommandResult(false, "", command.Notifications));
            }

            var filter = new BootstrapTableCommand()
            {
                Limit = command.PerPage,
                Offset = command.CurrentPage,
                Sort = command.OrderBy,
                Order = command.SortBy
            };

            var custumer = await _customerRepository.QueryPaginationAsync(filter, command);

            var customerPaginationDTO = new CustomerPaginationDTO();
            customerPaginationDTO.Customer.AddRange(custumer.Rows);
            customerPaginationDTO.PerPage = command.PerPage;
            customerPaginationDTO.CurrentPage = command.CurrentPage;
            customerPaginationDTO.LastPage = (custumer.Total / command.PerPage);
            customerPaginationDTO.Total = custumer.Total;

            return new GenericCommandResult(true, "", customerPaginationDTO);
        }

        private async Task<bool> ExistCustomer(string document, string email)
        {
            var existDocument = await _customerRepository.Get(t => t.Document == document.Trim());
            var existEmail = await _customerRepository.Get(t => t.Email == email.Trim());

            if (existDocument is not null || existEmail is not null)
            {
                return true;
            }

            return false;
        }
    }
}
