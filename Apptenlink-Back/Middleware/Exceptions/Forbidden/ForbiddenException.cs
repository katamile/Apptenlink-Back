using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Apptelink_Back.Middleware.Exceptions.Forbidden
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string mensaje) : base(mensaje)
        {
        }
    }
}
