using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Enums;
using WebApi.Ecommerce.Domain.Providers;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Domain.Responses;

namespace WebApi.Ecommerce.Services.Providers
{
    public class RequestProvider : IRequestProvider
    {
        private readonly ILogErroRepository _logErroRepository;

        public RequestProvider(ILogErroRepository logErroRepository)
        {
            _logErroRepository = logErroRepository;
        }

        #region Métodos de requisições
        public HttpResponse Execute(string url, HttpMethodEnum method, Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null, object body = null, HttpStatusCode? expectStatusCode = null)
        {
            var uri = BindParameters(url, parameters);
            var json = JsonConvert.SerializeObject(body);
            var data = MountContent(json);
            var client = new HttpClient();
            AddHeaders(ref client, headers);

            switch (method)
            {
                case HttpMethodEnum.Get:
                    {
                        var response = Get(client, uri);
                        client.Dispose();

                        return CheckStatusCodeAndReturn(response, expectStatusCode);
                    }

                case HttpMethodEnum.Post:
                    {
                        var response = Post(client, uri, data);
                        client.Dispose();

                        return CheckStatusCodeAndReturn(response, expectStatusCode);
                    }

                case HttpMethodEnum.Put:
                    {
                        var response = Put(client, uri, data);
                        client.Dispose();

                        return CheckStatusCodeAndReturn(response, expectStatusCode);
                    }

                case HttpMethodEnum.Patch:
                    {
                        var response = Patch(client, uri, data);
                        client.Dispose();

                        return CheckStatusCodeAndReturn(response, expectStatusCode);
                    }

                case HttpMethodEnum.Delete:
                    {
                        var response = Delete(client, uri);
                        client.Dispose();

                        return CheckStatusCodeAndReturn(response, expectStatusCode);
                    }

                default:
                    throw new NotImplementedException();
            }
        }

        public T Execute<T>(string url, HttpMethodEnum method, Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null, object body = null, HttpStatusCode? expectStatusCode = null)
        {
            var response = Execute(url, method, parameters, headers, body, expectStatusCode);

            return TryParse<T>(response);
        }

        public async Task<T> ExecuteAsync<T>(string url, HttpMethodEnum method, Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null, object body = null, HttpStatusCode? expectStatusCode = null)
        {
            var response = await ExecuteAsync(url, method, parameters, headers, body, expectStatusCode);

            return TryParse<T>(response);
        }

        public async Task<HttpResponse> ExecuteAsync(string url, HttpMethodEnum method, Dictionary<string, string> parameters = null,
            Dictionary<string, string> headers = null, object body = null, HttpStatusCode? expectStatusCode = null)
        {
            return await Task.Run(() => Execute(url, method, parameters, headers, body, expectStatusCode));
        }

        #endregion

        #region Métodos auxiliares para requisições.
        private HttpResponse Put(HttpClient client, Uri uri, StringContent data)
        {
            try
            {
                using (var response = client.PutAsync(uri, data))
                {
                    response.Wait();
                    return CreateResult(response);
                }
            }
            catch (Exception ex)
            {
                _logErroRepository.CreateAsync(new LogErro(method: string.Concat("PUT: ", this.GetType().Name), path: uri.ToString(), erro: ex.Message, erroCompleto: ex.ToString(), query: data.ToString()));
            }
            return new HttpResponse
            {
                StatusCode = HttpStatusCode.GatewayTimeout
            };
        }

        private HttpResponse Post(HttpClient client, Uri uri, StringContent data)
        {
            try
            {
                using (var response = client.PostAsync(uri, data))
                {
                    response.Wait();
                    return CreateResult(response);
                }
            }
            catch (Exception ex)
            {
                _logErroRepository.CreateAsync(new LogErro(method: string.Concat("POST: ", this.GetType().Name), path: uri.ToString(), erro: ex.Message, erroCompleto: ex.ToString(), query: data.ToString()));
            }
            return new HttpResponse
            {
                StatusCode = HttpStatusCode.GatewayTimeout
            };
        }

        private HttpResponse Get(HttpClient client, Uri uri)
        {
            try
            {
                using (var response = client.GetAsync(uri))
                {
                    response.Wait();
                    return CreateResult(response);
                }
            }
            catch (Exception ex)
            {
                _logErroRepository.CreateAsync(new LogErro(method: string.Concat("GET: ", this.GetType().Name), path: uri.ToString(), erro: ex.Message, erroCompleto: ex.ToString(), query: null));
            }
            return new HttpResponse
            {
                StatusCode = HttpStatusCode.GatewayTimeout
            };
        }

        private HttpResponse Patch(HttpClient client, Uri uri, StringContent data)
        {
            try
            {
                using (var response = client.PatchAsync(uri, data))
                {
                    response.Wait();
                    return CreateResult(response);
                }
            }
            catch (Exception ex)
            {
                _logErroRepository.CreateAsync(new LogErro(method: string.Concat("PATCH: ", this.GetType().Name), path: uri.ToString(), erro: ex.Message, erroCompleto: ex.ToString(), query: data.ToString()));
            }
            return new HttpResponse
            {
                StatusCode = HttpStatusCode.GatewayTimeout
            };
        }

        private HttpResponse Delete(HttpClient client, Uri uri)
        {
            try
            {
                using (var response = client.DeleteAsync(uri))
                {
                    response.Wait();
                    return CreateResult(response);
                }
            }
            catch (Exception ex)
            {
                _logErroRepository.CreateAsync(new LogErro(method: string.Concat("DELETE: ", this.GetType().Name), path: uri.ToString(), erro: ex.Message, erroCompleto: ex.ToString(), query: null));
            }
            return new HttpResponse
            {
                StatusCode = HttpStatusCode.GatewayTimeout
            };
        }

        private HttpResponse CreateResult(Task<HttpResponseMessage> response)
        {
            return new HttpResponse
            {
                StatusCode = response.Result.StatusCode,
                Content = response.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult(),
                ContentBytes = response.Result.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult()
            };
        }

        private T TryParse<T>(HttpResponse response)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                _logErroRepository.CreateAsync(new LogErro(method: string.Concat("TRY PARSE: ", this.GetType().Name), path: typeof(T).Name, erro: ex.Message, erroCompleto: ex.ToString(), query: null));
                return JsonConvert.DeserializeObject<T>(null);
            }
        }

        private void AddHeaders(ref HttpClient client, Dictionary<string, string> headers = null)
        {
            if (headers == null)
                return;

            foreach (KeyValuePair<string, string> item in headers)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
            }
        }

        private StringContent MountContent(string json, string mediaType = "application/json")
        {
            return new StringContent(json, Encoding.UTF8, mediaType);
        }

        private Uri BindParameters(string url, Dictionary<string, string> parameters = null)
        {
            if (parameters == null)
                return new Uri(url);

            return new Uri(QueryHelpers.AddQueryString(url, parameters));
        }
        #endregion

        #region Métodos auxiliares para requisições internas
        private HttpResponse CheckStatusCodeAndReturn(HttpResponse response, HttpStatusCode? expectStatusCode)
        {
            if (!expectStatusCode.HasValue)
                return response;

            if (expectStatusCode.Value != response.StatusCode)
            {
                _logErroRepository.CreateAsync(new LogErro(method: string.Concat("CHECK STATUS CODE AND RETURN: ", this.GetType().Name), path: null, erro: $"Fail Check Status Code And Return {response.StatusCode}.", erroCompleto: null, query: null));
            }

            return response;
        }
        #endregion
    }
}
