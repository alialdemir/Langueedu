using Ardalis.Result;
using Langueedu.Core.Entities.PlaylistAggregate;

namespace Langueedu.Core.Interfaces;

public interface IFollowerTrackService
{
  Task<Result<string>> Add(FollowerTrack followerTrack);
  Task<Result<string>> DeleteAsync(string userId, short trackId);
  Task<Result<bool>> IsFollowed(string userId, short trackId);
}

