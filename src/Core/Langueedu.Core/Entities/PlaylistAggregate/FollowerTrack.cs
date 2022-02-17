using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class FollowerTrack : BaseEntity, IAggregateRoot
{
  public short TrackId { get; set; }
  public string UserId { get; set; }
}

