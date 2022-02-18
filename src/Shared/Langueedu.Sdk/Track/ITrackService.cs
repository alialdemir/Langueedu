using System.Threading.Tasks;
using Langueedu.Sdk.Playlist.Response;

namespace Langueedu.Sdk.Track
{
  public interface ITrackService
  {
    Task<Result<bool>> FollowTrackAsync(int trackId);
    Task<Result<TrackDetailViewModel>> GetTrackDetailAsync(int trackId);
    Task<Result<bool>> UnFollowTrackAsync(int trackId);
  }
}