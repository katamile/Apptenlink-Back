﻿using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Apptelink_Back.Middleware.Exceptions.Unauthorized
{
    public class UnauthorizedException : UnauthorizedAccessException
    {
        public UnauthorizedException(string mensaje) : base(mensaje)
        {
        }
    }
}
