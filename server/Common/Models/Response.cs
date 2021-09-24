using System.Net;

namespace Common.Models
{
    public class Response<T>
    {
        public HttpStatusCode Status { get; set; }
        public bool IsSuccess { get => Status == HttpStatusCode.Accepted || Status==HttpStatusCode.OK;  }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class Response
    {
        public HttpStatusCode Status { get; set; }
        public bool IsSuccess { get => Status == HttpStatusCode.Accepted || Status==HttpStatusCode.OK;  }
        public string Message { get; set; }
    }
}