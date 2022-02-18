using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;

namespace Langueedu.Core.Specifications.Track;

public class GetFollowerTrackByUserIdSpec : Specification<FollowerTrack, FollowerTrack>
{
  public GetFollowerTrackByUserIdSpec(string userId, short trackId)
  {
    Query
      .Where(x => x.TrackId == trackId && x.UserId == userId);
  }
}
