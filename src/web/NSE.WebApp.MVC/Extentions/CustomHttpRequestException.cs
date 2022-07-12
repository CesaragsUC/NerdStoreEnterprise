using System;
using System.Net;

namespace NSE.WebApp.MVC.Extentions
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode StatusCode;

        public CustomHttpRequestException() { }

        public CustomHttpRequestException(string message,Exception innerExeException):base(message,innerExeException) {}

        public CustomHttpRequestException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
