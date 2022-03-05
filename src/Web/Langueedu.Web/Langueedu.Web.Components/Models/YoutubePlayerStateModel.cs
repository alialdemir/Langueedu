using System.Text.Json.Serialization;
using static Langueedu.Web.Components.Services.YoutubePlayer;

namespace Langueedu.Web.Components.Models;

public class YoutubePlayerStateModel
{
  [JsonPropertyName("data")]
  public PlayerState PlayerState { get; set; }
}

