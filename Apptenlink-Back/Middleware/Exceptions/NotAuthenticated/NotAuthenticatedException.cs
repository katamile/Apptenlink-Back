namespace Apptenlink_Back.Middleware.Exceptions.Unauthorized
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() : base("Es necesario autenticarse para obtener la respuesta solicitada.")
        {
        }
    }
}
