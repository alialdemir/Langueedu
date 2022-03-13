using System.Text.Json.Serialization;

namespace Langueedu.SharedKernel.ViewModels.Account;

public class TokenViewModel
{
  [JsonPropertyName("access_token")]
  public string AccessToken { get; set; }

  [JsonPropertyName("expires_in")]
  public int ExpiresIn { get; set; }

  [JsonPropertyName("token_type")]
  public string TokenType { get; set; }
}