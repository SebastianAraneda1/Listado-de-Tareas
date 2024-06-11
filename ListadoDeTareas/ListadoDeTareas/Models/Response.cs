using Microsoft.OpenApi.Any;

namespace ListadoDeTareas.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public String errorMessage { get; set; }
        public object data { get; set; }
    }
}
