using Langueedu.Core.Enums;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Artist : BaseEntity, IAggregateRoot
{
  public Artist(string name)
  {
    Name = name;
    Slug = name.GenerateSlug();
  }

  public ContentStatus ContentStatus { get; private set; } = ContentStatus.Passive;

  public string Name { get; private set; }

  public string Slug { get; private set; }

  private readonly List<Album> _albums = new();

  public string SpotifyId { get; private set; }

  public IReadOnlyCollection<Album> Albums => _albums.AsReadOnly();

  private readonly List<PerformsOnSong> _performsOnSongs = new();

  public IReadOnlyCollection<PerformsOnSong> PerformsOnSongs => _performsOnSongs.AsReadOnly();
  private readonly List<ArtistGenre> _artistGenres = new();

  public IReadOnlyCollection<ArtistGenre> ArtistGenres => _artistGenres.AsReadOnly();

  private readonly List<Image> _images = new();

  public IReadOnlyCollection<Image> Images => _images.AsReadOnly();

  public Artist AddImage(Image image)
  {
    _images.Add(image);

    return this;
  }

  public Artist AddPerformsOnSongs(Track track)
  {
    if (track is not null)
    {
      _performsOnSongs.Add(new PerformsOnSong
      {
        Artist = this,
        Track = track
      });
    }

    return this;
  }

  public Artist AddGenres(IEnumerable<Genre> genres)
  {
    if (genres is not null && genres.Any())
    {
      var filterGenres = genres
             .Where(x => !_artistGenres.Any(g => g.Genre.Description == x.Description))
             .Select(genre => new ArtistGenre
             {
               Artist = this,
               Genre = genre
             });

      _artistGenres.AddRange(filterGenres);
    }

    return this;
  }

  public Artist AddImages(IEnumerable<Image> images)
  {
    if (images is not null && images.Any())
    {
      _images.AddRange(images.Where(x => !_images.Any(i => i.Url == x.Url)));
    }

    return this;
  }

  public Artist AddAlbum(Album album)
  {
    _albums.Add(album);

    return this;
  }

  public Artist ChangeContentStatus(ContentStatus contentStatus)
  {
    ContentStatus = contentStatus;

    return this;
  }

  public Artist SetSpotifyId(string spotifyId)
  {
    SpotifyId = spotifyId;

    return this;
  }

  internal void AddGenres(IEnumerable<Task<Genre>> enumerable)
  {
    throw new NotImplementedException();
  }
}

