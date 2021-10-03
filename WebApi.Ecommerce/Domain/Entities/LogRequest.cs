using System;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class LogRequest : Entity
    {
        // Constructor
        public LogRequest()
        {

        }

        public LogRequest(string device,
            string host,
            string method,
            string path,
            string url,
            string header,
            string body,
            string query,
            string ip)
        {
            Device = device;
            Host = host;
            Method = method;
            Path = path;
            Url = url;
            Header = header;
            Body = body;
            Query = query;
            Ip = ip;
        }


        // Properties
        public string Device { get; private set; }

        public string Host { get; private set; }

        public string Method { get; private set; }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public string Header { get; private set; }

        public string Body { get; private set; }

        public string Query { get; private set; }

        public string Ip { get; private set; }

        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public TimeSpan ExecutionTime { get; private set; }


        // Modifier
        public void SetStatusCode(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public void SetResponse(string response)
        {
            this.Response = response;
        }

        public void SetExecutionTime(TimeSpan executionTime)
        {
            this.ExecutionTime = executionTime;
        }
    }
}
