using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Enums;
using WebApi.Ecommerce.Domain.Responses;

namespace WebApi.Ecommerce.Domain.Providers
{
    public interface IRequestProvider
    {
        T Execute<T>(string url, HttpMethodEnum method, Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null, object body = null, HttpStatusCode? expectStatusCode = null);

        HttpResponse Execute(string url, HttpMethodEnum method, Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null, object body = null, HttpStatusCode? expectStatusCode = null);

        Task<T> ExecuteAsync<T>(string url, HttpMethodEnum method, Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null, object body = null, HttpStatusCode? expectStatusCode = null);

        Task<HttpResponse> ExecuteAsync(string url, HttpMethodEnum method, Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null, object body = null, HttpStatusCode? expectStatusCode = null);
    }
}
