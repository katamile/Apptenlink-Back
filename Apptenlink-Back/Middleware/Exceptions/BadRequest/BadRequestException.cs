using System.Net;

namespace Apptenlink_Back.Middleware.Exceptions.BadRequest
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensaje) : base(mensaje)
        {
        }
    }
}
