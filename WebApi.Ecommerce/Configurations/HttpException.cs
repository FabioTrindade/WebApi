using System;
using System.Net;
using WebApi.Ecommerce.Domain.Commands;

namespace WebApi.Ecommerce.Configurations
{
    public class HttpException : Exception
    {
        public HttpException(HttpStatusCode statusCode, GenericCommandResult result)
        {
            Result = result;
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public GenericCommandResult Result { get; private set; }
    }
}
