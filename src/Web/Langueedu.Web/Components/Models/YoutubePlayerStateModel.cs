using System.Text.Json.Serialization;
using static Langueedu.Components.Services.YoutubePlayer;

namespace Langueedu.Components.Models;

public class YoutubePlayerStateModel
{
  [JsonPropertyName("data")]
  public PlayerState PlayerState { get; set; }
}

