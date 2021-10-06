using WebApi.Ecommerce.Domain.Commands.Other;
using WebApi.Ecommerce.Domain.Services;

namespace WebApi.Ecommerce.Domain.Providers
{
    public interface IZipCodeProvider : IService<ZipCodeCommand>
        , IService<ShippingWithZipCodeCommand>
    {
    }
}
