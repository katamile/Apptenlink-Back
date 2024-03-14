using System.Text.Json.Serialization;

namespace Apptelink_Back.Middleware.Models
{
    public class JWTRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
