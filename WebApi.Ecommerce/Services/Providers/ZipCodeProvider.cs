﻿using System.Threading.Tasks;
using WebApi.Ecommerce.Commons;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Other;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.Enums;
using WebApi.Ecommerce.Domain.Providers;
using WebApi.Ecommerce.Domain.Responses;
using WebApi.Ecommerce.Extensions;

namespace WebApi.Ecommerce.Services.Providers
{
    public class ZipCodeProvider : IZipCodeProvider
    {
        private readonly IRequestProvider _requestProvider;

        public ZipCodeProvider(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<GenericCommandResult> Handle(ZipCodeCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "", command.Notifications);
            }

            var result = await AddressWithZipCode(command.ZipCode);
            result.ZipCode.ValidateIfIsNull($"Cep {command.ZipCode} não encontrado.");

            var address = new AddressDTO(result.ZipCode, result.Address, result.Complement, result.Neighborhood, result.City, result.State, result.Ddd);

            return new GenericCommandResult(true, "", address);
        }

        public async Task<GenericCommandResult> Handle(ShippingWithZipCodeCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "", command.Notifications);
            }

            var address = await AddressWithZipCode(command.ZipCode);
            address.ZipCode.ValidateIfIsNull($"Cep {command.ZipCode} não encontrado.");

            var shipping = await ShippingWithZipCode(address.City, address.State);

            return new GenericCommandResult(true, "", shipping);
        }

        private async Task<ZipCodeResponse> AddressWithZipCode(string zipCode)
        {
            var request = await _requestProvider.ExecuteAsync(
                    url: string.Format(Settings.ViaCep, zipCode),
                    method: HttpMethodEnum.Get
                );

            var address = Utils.DeserializeObject<ZipCodeResponse>(request.Content);

            return address;
        }

        private async Task<ShippingDTO> ShippingWithZipCode(string city, string state)
        {
            var shipping = new ShippingDTO();

            if(Settings.State == state)
            {
            }

            return shipping;
        }
    }
}
