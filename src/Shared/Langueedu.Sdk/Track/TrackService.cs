using System.Net.Http;
using System.Threading.Tasks;
using Langueedu.Sdk.Playlist.Response;
using Langueedu.Sdk.Utilities;

namespace Langueedu.Sdk.Track;

public class TrackService : ServiceBase, ITrackService
{
  private const string API_URL_BASE = "/v1/Tracks";

  public TrackService(IHttpClientFactory clientFactory)
      : base(clientFactory.CreateClient("LangueeduApi"))
  {
  }

  public async Task<Result<TrackDetailViewModel>> GetTrackDetail(int trackId)
  {
    if (trackId <= 0)
      return Result<TrackDetailViewModel>.Invalid("Track id cannot be zero or less than zero.", nameof(trackId));

    string uri = UriHelper.CombineUri(Configs.GatewaEndpoint, API_URL_BASE, trackId.ToString());

    var response = await GetAsync<TrackDetailViewModel>(uri);
    return response;
  }

  public async Task<Result<bool>> FollowTrack(int trackId)
  {
    if (trackId <= 0)
      return Result<bool>.Invalid("Track id cannot be zero or less than zero.", nameof(trackId));

    string uri = UriHelper.CombineUri(Configs.GatewaEndpoint, $"{API_URL_BASE}/{trackId}/Follow", trackId.ToString());

    var response = await PostAsync<bool>(uri);
    return response;
  }

  public async Task<Result<bool>> UnFollowTrack(int trackId)
  {
    if (trackId <= 0)
      return Result<bool>.Invalid("Track id cannot be zero or less than zero.", nameof(trackId));

    string uri = UriHelper.CombineUri(Configs.GatewaEndpoint, $"{API_URL_BASE}/{trackId}/Follow", trackId.ToString());

    var response = await DeleteAsync<bool>(uri);
    return response;
  }
}
