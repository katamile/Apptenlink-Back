using Microsoft.AspNetCore.Http;
using System.Net;

namespace Apptenlink_Back.Middleware.Exceptions.NotImplemented
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException(string mensaje) : base(mensaje)
        {
        }
    }
}
