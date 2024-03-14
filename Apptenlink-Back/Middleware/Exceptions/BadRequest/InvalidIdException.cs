namespace Apptelink_Back.Middleware.Exceptions.BadRequest
{
    public class InvalidIdException : BadRequestException
    {
        public InvalidIdException() : base("Id no válido.")
        {
        }
    }
}