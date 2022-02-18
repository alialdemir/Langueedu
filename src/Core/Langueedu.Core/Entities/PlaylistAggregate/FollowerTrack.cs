using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class FollowerTrack : BaseEntity, IAggregateRoot
{
  public FollowerTrack(short trackId, string userId)
  {
    TrackId = trackId;
    UserId = userId;
  }
  public FollowerTrack()
  {

  }
  public short TrackId { get; set; }

  public string UserId { get; set; }
}

