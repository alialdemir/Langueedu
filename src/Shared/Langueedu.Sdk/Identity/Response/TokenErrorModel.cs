using System.Text.Json.Serialization;

namespace Langueedu.Sdk.Identity.Response
{
    public class TokenErrorModel
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }
    }
}

