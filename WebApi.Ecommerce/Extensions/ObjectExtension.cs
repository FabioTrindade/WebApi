using WebApi.Ecommerce.Configurations;

namespace WebApi.Ecommerce.Extensions
{
    public static class ObjectExtension
    {
        public static void ValidateIfIsNull(this object obj, string errorMessage = null)
        {
            if (obj is null)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, new Domain.Commands.GenericCommandResult(false, string.IsNullOrEmpty(errorMessage) ? "Objecto não encontrado" : errorMessage));
            }
        }
    }
}
