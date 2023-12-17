using System.Net;

namespace ApiBlog.Models
{
    public class RespuestasApi
    {

        public RespuestasApi()
        {
            ErrorMessages = new List<string>(); 
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
