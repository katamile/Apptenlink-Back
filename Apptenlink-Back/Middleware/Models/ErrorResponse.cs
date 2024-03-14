
 using System.Net;

namespace Apptenlink_Back.Middleware.Models
{
    internal class ErrorResponse
    {
        public HttpStatusCode code { get; set; }
        public string message { get; set; }
        public string exception { get; set; }

    }
}