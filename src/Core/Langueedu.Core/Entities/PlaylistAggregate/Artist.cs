using Langueedu.Core.Enums;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Artist : BaseEntity, IAggregateRoot
{
    public Artist(string name, string picturePath)
    {
        Name = name;
        PicturePath = picturePath;
        Slug = name.GenerateSlug();
    }

    public ContentStatus ContentStatus { get; private set; } = ContentStatus.Passive;

    public string Name { get; private set; }

    public string Slug { get; private set; }

    private readonly List<Album> _albums = new();

    public IReadOnlyCollection<Album> Albums => _albums.AsReadOnly();

    public string PicturePath { get; set; }

    private readonly List<PerformsOnSong> _performsOnSongs = new();

    public IReadOnlyCollection<PerformsOnSong> PerformsOnSongs => _performsOnSongs.AsReadOnly();

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
}

