using Langueedu.Core.Entities.CourseAggregate;
using Langueedu.Core.Entities.LanguageAggregate;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;
using Langueedu.SharedKernel.ViewModels.Playlist;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Track : BaseEntity<short>, IAggregateRoot
{
  public Track(string name, string youtubeVideoId, int duration)
  {
    Name = name;
    Slug = name.GenerateSlug();
    YoutubeVideoId = youtubeVideoId;
    Duration = duration;
  }

  public string Name { get; private set; }

  public int Duration { get; private set; }

  public string Slug { get; private set; }

  public string YoutubeVideoId { get; private set; }

  private string _youtubeImageUrl = "https://img.youtube.com/vi/";

  public IEnumerable<ImageViewModel> Images
  {
    get
    {
      return new List<ImageViewModel>
      {
        new ImageViewModel{ Url = $"{_youtubeImageUrl}{YoutubeVideoId}/hqdefault.jpg", Width = 480, Height=360},
        new ImageViewModel{ Url = $"{_youtubeImageUrl}{YoutubeVideoId}/mqdefault.jpg", Width = 320, Height=180}
      };
    }
  }

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

  public TrackLevel TrackLevel { get; set; } = TrackLevel.Easy;

  public int Hits { get; set; }

  public byte DisplayOrder { get; set; }

  public PlayList Playlist { get; set; }

  public Album Album { get; set; }

  public Language Language { get; set; }

  private readonly List<PerformsOnSong> _performsOnSongs = new();

  public IReadOnlyCollection<PerformsOnSong> PerformsOnSongs => _performsOnSongs.AsReadOnly();

  private readonly List<FollowerTrack> _followerTracks = new();

  public IReadOnlyCollection<FollowerTrack> FollowerTracks => _followerTracks.AsReadOnly();

  private readonly List<Lyrics> _lyrics = new();

  public IReadOnlyCollection<Lyrics> Lyrics => _lyrics.AsReadOnly();

  private readonly List<Course> _courses = new();

  public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

  public string? SpotifyId { get; private set; }

  public Track SetSpotifyId(string? spotifyId)
  {
    SpotifyId = spotifyId;

    return this;
  }

  public Track AddLyrics(IEnumerable<Lyrics> lyrics)
  {
    _lyrics.AddRange(lyrics);

    return this;
  }

  public Track AddPerformsOnSongs(IEnumerable<Artist> artists)
  {
    if (artists is not null && artists.Any())
    {
      foreach (var artist in artists)
      {
        _performsOnSongs.Add(new PerformsOnSong
        {
          Artist = artist,
          Track = this
        });
      }
    }

    return this;
  }

  public Track AddFollowerTrack(FollowerTrack followerTrack)
  {
    _followerTracks.Add(followerTrack);

    FollowerCount += 1;

    return this;
  }

  public Track SetHits(int hits)
  {
    Hits = hits;

    return this;
  }

  public Track SetFollowerCount(long followerCount)
  {
    FollowerCount = followerCount;

    return this;
  }

  public Track SetTrackLevel(TrackLevel trackLevel)
  {
    TrackLevel = trackLevel;

    return this;
  }


  public Track AddCourse(Course course)
  {
    _courses.Add(course);

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

  public Track SetLang(Language language)
  {
    Language = language;

    return this;
  }

  public Track SetLangCc(string langCc)
  {
    if (Language is null)
      Language = new Language(langCc);

    Language.SetLangCc(langCc);

    return this;
  }
}

