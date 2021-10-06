using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;

namespace WebApi.Ecommerce.Middlewares
{
    public class RequestAndResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private const string StopwatchKey = "StopwatchFilter.Value";

        public RequestAndResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IHttpContextAccessor httpContext, ILogRequestRepository _logRequestRepository)
        {
            var logRequestObject = new LogRequest();

            //code dealing with request
            var logRequest = await FormatRequest(httpContext, logRequestObject);

            try
            {
                await _logRequestRepository.CreateAsync(logRequest);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Erro em gravar a requisição", ex);
            }

            var originalBodyStream = httpContext.HttpContext.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                httpContext.HttpContext.Response.Body = responseBody;

                await _next(context);

                //code dealing with response
                var logResponse = await FormatResponse(httpContext.HttpContext.Response, logRequestObject);
                await responseBody.CopyToAsync(originalBodyStream);

                try
                {
                    await _logRequestRepository.UpdateAsync(logRequest);
                }
                catch (System.Exception ex)
                {
                    throw new Exception("Erro em atualizar registro de requisição", ex);
                }
            }
        }


        private async Task<LogRequest> FormatRequest(IHttpContextAccessor httpContext, LogRequest logRequest)
        {
            // Set time start request
            httpContext.HttpContext.Items[StopwatchKey] = Stopwatch.StartNew();

            //  Enable seeking
            httpContext.HttpContext.Request.EnableBuffering();
            //context.Request.EnableBuffering();
            //  Read the stream as text
            var bodyAsText = await new StreamReader(httpContext.HttpContext.Request.Body).ReadToEndAsync();
            //  Set the position of the stream to 0 to enable rereading
            httpContext.HttpContext.Request.Body.Position = 0;

            var device = httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Trim();
            string host = httpContext.HttpContext.Request.Host.ToString().Trim();
            string method = httpContext.HttpContext.Request?.Method.ToString().Trim();
            string path = httpContext.HttpContext.Request?.Path.ToString().Trim();
            string query = WebUtility.UrlDecode(httpContext.HttpContext.Request?.QueryString.ToString().Trim());
            string header = Newtonsoft.Json.JsonConvert.SerializeObject(httpContext.HttpContext.Request.Headers).ToString().Trim();
            string body = bodyAsText.ToString().Trim();
            string ip = httpContext.HttpContext.Connection.RemoteIpAddress.ToString().Trim();
            string url = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}{httpContext.HttpContext.Request.Path}{httpContext.HttpContext.Request.QueryString}";

            logRequest.SetDevice(device);
            logRequest.SetHost(host);
            logRequest.SetMethod(method);
            logRequest.SetPath(path);
            logRequest.SetUrl(url);
            logRequest.SetHeader(header);
            logRequest.SetBody(body);
            logRequest.SetQuery(query);
            logRequest.SetIp(ip);

            return logRequest;
        }

        private async Task<LogRequest> FormatResponse(HttpResponse response, LogRequest logRequest)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            Stopwatch stopwatch = (Stopwatch)response.HttpContext.Items[StopwatchKey];

            var statusCode = response.StatusCode;
            var resultResponse = text.ToString().Trim();

            logRequest.SetExecutionTime(stopwatch.Elapsed);
            logRequest.SetStatusCode(statusCode);
            logRequest.SetResponse(resultResponse);
            logRequest.SetUpdatedAt(DateTime.Now);

            return logRequest;
        }
    }
}
