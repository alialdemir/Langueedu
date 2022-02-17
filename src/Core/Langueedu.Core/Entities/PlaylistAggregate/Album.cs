using Langueedu.Core.Enums;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Album : BaseEntity, IAggregateRoot
{
  public Album(string name)
  {
    Name = name;
    Slug = name.GenerateSlug();
  }

  public ContentStatus ContentStatus { get; private set; } = ContentStatus.Passive;

  public string? AlbumCoverImage { get; set; }

  public string Name { get; private set; }

  public string Slug { get; private set; }

  public DateTime ReleaseDate { get; private set; }

  private readonly List<Track> _tracks = new();

  public IReadOnlyCollection<Track> Tracks => _tracks.AsReadOnly();

  public Artist MainArtist { get; set; }

  public Album ChangeContentStatus(ContentStatus contentStatus)
  {
    ContentStatus = contentStatus;

    return this;
  }

  public Album AddTrack(Track track)
  {
    _tracks.Add(track);

    return this;
  }

  public Album SetReleaseDate(DateTime releaseDate)
  {
    ReleaseDate = releaseDate;

    return this;
  }
}

