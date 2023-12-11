using System.Net;

namespace Todo_App.Models
{
    public class ResponseDto
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public string Message { get; set; }

        public object Result { get; set; } = default;
    }
}
