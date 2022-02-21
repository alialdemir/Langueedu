using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Genre : BaseEntity, IAggregateRoot
{
  public Genre(string description)
  {
    Description = description;
    Slug = description.GenerateSlug();
  }

  public Genre()
  {

  }

  public string Description { get; }
  public string Slug { get; private set; }

  private readonly List<Artist> _artists = new();
  public IReadOnlyCollection<Artist> Artists => _artists.AsReadOnly();

  public Genre AddArtist(Artist artist)
  {
    _artists.Add(artist);

    return this;
  }

  private readonly List<Album> _albums = new();
  public IReadOnlyCollection<Album> Albums => _albums.AsReadOnly();

  public Genre AddAlbum(Album album)
  {
    _albums.Add(album);

    return this;
  }
}

