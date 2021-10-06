using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApi.Ecommerce.Configurations;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;

namespace WebApi.Ecommerce.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogErroRepository logErroRepository /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logErroRepository);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, ILogErroRepository logErroRepository)
        {
            var code = HttpStatusCode.InternalServerError;
            GenericCommandResult result;
            if (exception is Exception) code = HttpStatusCode.InternalServerError;

            if (exception is HttpException)
            {
                var httpException = exception as HttpException;
                code = httpException.StatusCode;
                result = httpException.Result;
            }
            else
            {
                result = new GenericCommandResult(false, exception.InnerException != null ? exception.InnerException.Message : exception.Message);
            }

            var resultSerialized = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var erro = new LogErro();

            erro.SetMethod(context.Request.Method.ToString());
            erro.SetPath(string.Concat(context.Request.Path.ToString(), (context.Request.QueryString.Value != null) ? context.Request.QueryString.Value.ToString() : null));
            erro.SetErroCompleto(exception.StackTrace);

            logErroRepository.CreateAsync(erro).GetAwaiter().GetResult();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(resultSerialized);
        }
    }
}
