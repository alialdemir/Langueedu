using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class ArtistGenre : BaseEntity, IAggregateRoot
{
  public Genre Genre { get; set; }

  public Artist Artist { get; set; }
}

