using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;
using System.Linq;

namespace Langueedu.Core.Specifications.Track;

public class GetFollowerTrackByUserIdAndIdSpec : Specification<FollowerTrack, FollowerTrack>
{
  public GetFollowerTrackByUserIdAndIdSpec(string userId, short trackId)
  {
    Query
      .Where(x => x.TrackId == trackId && x.UserId == userId);

    Query
      .Select(x => x);
  }
}
