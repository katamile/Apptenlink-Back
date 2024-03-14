using System.Text.Json.Serialization;

namespace Apptenlink_Back.Middleware.Models
{
    public class JWTRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
