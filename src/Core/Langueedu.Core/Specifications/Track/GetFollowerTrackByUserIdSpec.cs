using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;

namespace Langueedu.Core.Specifications.Track;

public class GetFollowerTrackByUserIdSpec : Specification<FollowerTrack, FollowerTrack>
{
  public GetFollowerTrackByUserIdSpec(string userId, int trackId)
  {
    Query
      .Where(x => x.Id == trackId && x.UserId == userId);
  }
}