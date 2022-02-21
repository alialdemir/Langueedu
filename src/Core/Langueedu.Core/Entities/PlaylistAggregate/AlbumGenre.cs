using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class AlbumGenre : BaseEntity, IAggregateRoot
{
  public Genre Genre { get; set; }

  public Album Album { get; set; }
}

