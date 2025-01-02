using Newtonsoft.Json;
using System.Net;

namespace ERP.PURCHASES.Dto
{

    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public string Message {  get; set; }    
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }

}