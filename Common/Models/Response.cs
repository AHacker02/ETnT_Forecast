using System.Net;

namespace Common.Models
{
    public class Response<T>
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class Response
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}