using Ardalis.Result;
using Langueedu.Core.Entities.PlaylistAggregate;

namespace Langueedu.Core.Interfaces;

public interface IFollowerTrackService
{
  Task<Result<bool>> Add(FollowerTrack followerTrack);
  Task<Result<bool>> DeleteAsync(string userId, short trackId);
  Task<Result<bool>> IsFollowed(string userId, short trackId);
}

