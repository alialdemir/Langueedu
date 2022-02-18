using Ardalis.Result;
using Langueedu.Core.Entities.PlaylistAggregate;

namespace Langueedu.Core.Interfaces;

public interface IFollowerTrackService
{
  Task<Result<bool>> AddAsync(FollowerTrack followerTrack);
  Task<Result<bool>> DeleteAsync(FollowerTrack followerTrack);
}

