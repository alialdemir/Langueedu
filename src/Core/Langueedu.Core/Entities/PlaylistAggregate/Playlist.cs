using Ardalis.GuardClauses;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class PlayList : BaseEntity<short>, IAggregateRoot
{
  public PlayList(string name)
  {
    Name = name;
    Slug = name.GenerateSlug();
  }

  public byte DisplayOrder { get; set; }

  public string Slug { get; private set; }

  public string Name { get; private set; }
  private readonly List<Track> _tracks = new();
  public IReadOnlyCollection<Track> Tracks => _tracks.AsReadOnly();

  public ContentStatus ContentStatus { get; private set; } = ContentStatus.Passive;

  public PlayList AddTrack(Track track)
  {
    Guard.Against.Null(track, nameof(track));

    _tracks.Add(track);

    return this;
  }

  public PlayList ChangeContentStatus(ContentStatus contentStatus)
  {
    ContentStatus = contentStatus;

    return this;
  }

  public void UpdateName(string newName)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  }
}
