using System.Threading.Tasks;
using Langueedu.Sdk.Playlist.Response;

namespace Langueedu.Sdk.Track
{
    public interface ITrackService
    {
        Task<Result<TrackDetailViewModel>> GetTrackDetail(int trackId);
    }
}