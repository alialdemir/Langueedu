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

  public string Name { get; private set; }

  public string Slug { get; private set; }

  private readonly List<Track> _tracks = new();

  public IReadOnlyCollection<Track> Tracks => _tracks.AsReadOnly();

  private readonly List<Image> _images = new();
  public IReadOnlyCollection<Image> Images => _images.AsReadOnly();

  public string SpotifyId { get; private set; }

  private readonly List<AlbumGenre> _albumGenres = new();

  public IReadOnlyCollection<AlbumGenre> AlbumGenres => _albumGenres.AsReadOnly();

  public Album AddGenre(Genre genre)
  {
    if (genre is not null)
    {
      _albumGenres.Add(new AlbumGenre
      {
        Album = this,
        Genre = genre
      });
    }

    return this;
  }


  public Album SetSpotifyId(string spotifyId)
  {
    SpotifyId = spotifyId;

    return this;
  }

  public Album AddGenres(IEnumerable<Genre> genres)
  {
    if (genres is not null && genres.Any())
    {
      var filterGenres = genres
             .Where(x => !_albumGenres.Any(g => g.Genre.Description == x.Description))
             .Select(genre => new AlbumGenre
             {
               Album = this,
               Genre = genre
             });

      _albumGenres.AddRange(filterGenres);
    }

    return this;
  }

  public Album AddImages(IEnumerable<Image> images)
  {
    if (images is not null && images.Any())
    {
      _images.AddRange(images.Where(x => !_images.Any(i => i.Url == x.Url)));
    }

    return this;
  }

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
}

