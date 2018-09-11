using System.Net;

namespace PutridParrot.Http
{
    public class HttpResponse<T>
    {
        public HttpResponse(
            T response,
            HttpStatusCode statusCode,
            string statusDescription)
        {
            Response = response;
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        public HttpStatusCode StatusCode { get; }
        public string StatusDescription { get; }
        public T Response { get; }
    }
}
