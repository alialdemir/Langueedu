using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class PerformsOnSong : BaseEntity, IAggregateRoot
{
  public Track Track { get; set; }

  public Artist Artist { get; set; }
}
