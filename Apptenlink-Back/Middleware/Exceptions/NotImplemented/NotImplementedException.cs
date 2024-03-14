using Microsoft.AspNetCore.Http;
using System.Net;

namespace Apptelink_Back.Middleware.Exceptions.NotImplemented
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException(string mensaje) : base(mensaje)
        {
        }
    }
}
