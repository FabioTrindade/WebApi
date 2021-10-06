using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Commands.Other;
using WebApi.Ecommerce.Domain.Enums;
using WebApi.Ecommerce.Domain.Providers;

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
            var result = await _requestProvider.ExecuteAsync(
                    url: string.Format("https://viacep.com.br/ws/{0}/json/", "78068050"),
                    method: HttpMethodEnum.Get
                );

            var address = 1;

            return new GenericCommandResult(true, "", address);
        }
    }
}
