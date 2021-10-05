using System.Net;

namespace WebApi.Ecommerce.Domain.Responses
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }

        public byte[] ContentBytes { get; set; }
    }
}
