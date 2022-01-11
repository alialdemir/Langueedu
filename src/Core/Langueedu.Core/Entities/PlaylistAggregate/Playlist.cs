using Ardalis.GuardClauses;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Playlist : BaseEntity<short>, IAggregateRoot
{
    public Playlist(string playlistName)
    {
        PlaylistName = playlistName;
        Slug = playlistName.GenerateSlug();
    }

    public byte DisplayOrder { get; set; }

    public string Slug { get; private set; }

    public string PlaylistName { get; private set; }

    private readonly List<Track> _tracks = new();

    public IReadOnlyCollection<Track> Tracks => _tracks.AsReadOnly();

    public ContentStatus ContentStatus { get; private set; } = ContentStatus.Passive;

    public Playlist AddTrack(Track track)
    {
        Guard.Against.Null(track, nameof(track));

        _tracks.Add(track);

        return this;
    }

    public Playlist ChangeContentStatus(ContentStatus contentStatus)
    {
        ContentStatus = contentStatus;

        return this;
    }

    public void UpdateName(string newName)
    {
        PlaylistName = Guard.Against.NullOrEmpty(newName, nameof(newName));
    }
}