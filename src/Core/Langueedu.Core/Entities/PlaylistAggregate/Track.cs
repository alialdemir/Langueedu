using Langueedu.Core.Enums;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Track : BaseEntity<short>, IAggregateRoot
{
  public Track(string songTitle, string youtubeVideoId, string lang, float duration)
  {
    SongTitle = songTitle;
    Slug = songTitle.GenerateSlug();
    YoutubeVideoId = youtubeVideoId;
    Lang = lang;
    Duration = duration;
  }

  public Track()
  {
  }

  public string SongTitle { get; private set; }

  public float Duration { get; private set; }

  public string Slug { get; private set; }

  public string YoutubeVideoId { get; private set; }

  public string PicturePath { get => $"https://img.youtube.com/vi/{YoutubeVideoId}/mqdefault.jpg"; }

  public string Lang { get; private set; }

  public string? LangCc { get; private set; }
  private long _followerCount;
  public long FollowerCount
  {
    get { return _followerCount; }
    set
    {
      if (value >= 0) _followerCount = value;
    }
  }

  public ContentStatus ContentStatus { get; private set; } = ContentStatus.Passive;

  public byte DisplayOrder { get; set; }

  public Playlist Playlist { get; set; }
  public Album Album { get; set; }

  private readonly List<PerformsOnSong> _performsOnSongs = new();

  public IReadOnlyCollection<PerformsOnSong> PerformsOnSongs => _performsOnSongs.AsReadOnly();

  private readonly List<FollowerTrack> _followerTracks = new();

  public IReadOnlyCollection<FollowerTrack> FollowerTracks => _followerTracks.AsReadOnly();

  private readonly List<Lyrics> _lyrics = new();

  public IReadOnlyCollection<Lyrics> Lyrics => _lyrics.AsReadOnly();

  public Track AddPerformsOnSong(Artist artist)
  {
    _performsOnSongs.Add(new PerformsOnSong
    {
      Artist = artist,
      Track = this
    });

    return this;
  }

  public Track AddFollowerTrack(FollowerTrack followerTrack)
  {
    _followerTracks.Add(followerTrack);

    FollowerCount += 1;

    return this;
  }

  public Track RemoveFollowerTrack(FollowerTrack followerTrack)
  {
    _followerTracks.RemoveAll(x => x.TrackId == followerTrack.TrackId && x.UserId == followerTrack.UserId);

    if (FollowerCount > 0)
      FollowerCount -= 1;

    return this;
  }

  public Track ChangeContentStatus(ContentStatus contentStatus)
  {
    ContentStatus = contentStatus;

    return this;
  }

  public Track SetLangCc(string langCc)
  {
    LangCc = langCc;

    return this;
  }
}

