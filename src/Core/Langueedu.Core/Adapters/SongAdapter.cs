using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using Langueedu.Core.Entities.LanguageAggregate;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.Core.Specifications.AlbumSpec;
using Langueedu.Core.Specifications.ArtistSpec;
using Langueedu.Core.Specifications.TrackSpec;
using Langueedu.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using SpotifyAPI.Web;
using Image = Langueedu.Core.Entities.PlaylistAggregate.Image;


namespace Langueedu.Core.Adapters;

public class TrackAdapter : ITrackAdapter
{
  private SpotifyClient _spotifyClient;
  private readonly IRepository<PlayList> _playlistRepository;
  private readonly IRepository<Track> _trackRepository;
  private readonly IRepository<Genre> _genreRepository;
  private readonly IRepository<Album> _albumRepository;
  private readonly IRepository<Language> _languageRepository;
  private readonly IRepository<Artist> _artstRepository;

  public TrackAdapter(IOptions<SpotifySettings> spotifySettings,
                      IRepository<PlayList> playlistRepository,
                      IRepository<Artist> artstRepository,
                      IRepository<Track> trackRepository,
                      IRepository<Genre> genreRepository,
                      IRepository<Album> albumRepository,
                      IRepository<Language> languageRepository)
  {
    Guard.Against.NullOrEmpty(spotifySettings.Value.SpotifyClientId, nameof(spotifySettings.Value.SpotifyClientId));
    Guard.Against.NullOrEmpty(spotifySettings.Value.SpotifySecretId, nameof(spotifySettings.Value.SpotifySecretId));

    var token = GetSpotifyAccessToken(spotifySettings.Value.SpotifyClientId, spotifySettings.Value.SpotifySecretId);
    _spotifyClient = new SpotifyClient(token.AccessToken, token.TokenType);

    _playlistRepository = playlistRepository;
    _trackRepository = trackRepository;
    _genreRepository = genreRepository;
    _albumRepository = albumRepository;
    _languageRepository = languageRepository;
    _artstRepository = artstRepository;
  }

  public async Task<PlayList> GetTracks(string playlistName, List<Song> songs)
  {
    var playList = new PlayList(playlistName).ChangeContentStatus(ContentStatus.Active);
    await _playlistRepository.AddAsync(playList);

    var errorSongs = new string[] { "HilzhbW4Ai" };

    var allGenres = await _genreRepository.ListAsync();
    var allLanguages = await _languageRepository.ListAsync();

    foreach (var song in songs.Where(x => !errorSongs.Any((y) => y == x.LyricsId)))
    {
      try
      {
        var tracksSpotify = await _spotifyClient.Search.Item(new SearchRequest(SearchRequest.Types.Track, song.Title));
        //if (tracksSpotify == null || tracksSpotify.Tracks == null || !tracksSpotify.Tracks.Items.Any())
        //  continue;

        string trackSpotifyId = tracksSpotify.Tracks.Items?.FirstOrDefault()?.Id ?? "";
        var trackSpotify = await _spotifyClient.Tracks.Get(trackSpotifyId);
        var albumSpotify = await _spotifyClient.Albums.Get(trackSpotify.Album.Id);
        var artistSpotify = await _spotifyClient.Artists.Get(trackSpotify?.Artists?.FirstOrDefault()?.Id ?? "");

        var trackSearchSpec = new SearchTrackSpec(trackSpotify.Name);
        var track = await _trackRepository.GetBySpecAsync(trackSearchSpec);
        if (track is null)
          track = new Track(song.Title, song.YoutubeVideoId, song.Duration);

        track
         .ChangeContentStatus(ContentStatus.Active)
         .SetSpotifyId(trackSpotify.Id)
         .SetHits(artistSpotify.Followers.Total)
         .SetFollowerCount(artistSpotify.Followers.Total)
         .SetTrackLevel(ConvertToTrackLevelEnum(song.Level))
         .AddLyrics(song.Lyrics.Select(x =>
         {
           (string answer, int? answerIndex) = GetRandomAnswer(x.Text);
           return new Lyrics(x.Text, answer, answerIndex, x.Time);
         }));


        var language = allLanguages.FirstOrDefault(l => l.Lang == song.Lang);
        if (language is null)
        {
          track.SetLang(new Language(song.Lang, song.LangCc));
          allLanguages.Add(track.Language);
        }
        else
          track.SetLang(language);

        var albumSearchSpec = new SearchAlbumSpec(albumSpotify.Name);
        var album = await _albumRepository.GetBySpecAsync(albumSearchSpec);
        if (album is null)
          album = new Album(albumSpotify.Name);

        album
         .ChangeContentStatus(ContentStatus.Active)
         .SetSpotifyId(albumSpotify.Id)
         .AddGenres(artistSpotify.Genres.Select(genre => GetGenre(genre, allGenres)))
         .AddImages(albumSpotify.Images.Select(x => new Image(x.Url, x.Width, x.Height)))
         .AddTrack(track);

        var artistSearchSpec = new SearchArtistSpec(artistSpotify.Name);
        var artist = await _artstRepository.GetBySpecAsync(artistSearchSpec);
        if (artist is null)
          artist = new Artist(artistSpotify.Name);

        artist
         .ChangeContentStatus(ContentStatus.Active)
         .SetSpotifyId(artistSpotify.Id)
         .AddGenres(artistSpotify.Genres.Select(genre => GetGenre(genre, allGenres)))
         .AddImages(artistSpotify.Images.Select(x => new Image(x.Url, x.Width, x.Height)))
         .AddPerformsOnSongs(track)
         .AddAlbum(album);

        Console.WriteLine($"track: {track.Name} - artist: {artist.Name} - album: {album.Name}");

        if (playList.Tracks.Count() >= 10)// test
          break;

        if (artist.Id > 0)
          await _artstRepository.UpdateAsync(artist);
        else
          await _artstRepository.AddAsync(artist);

        if (album.Id > 0)
          await _albumRepository.UpdateAsync(album);
        else
          await _albumRepository.AddAsync(album);

        if (track.Id > 0)
          await _trackRepository.UpdateAsync(track);
        else
          await _trackRepository.AddAsync(track);

        await _playlistRepository.SaveChangesAsync();

        playList.AddTrack(track);

        await Task.Delay(1000);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    Console.WriteLine(playlistName);

    return playList;
  }

  private Genre GetGenre(string genreName, List<Genre> allGenres)
  {
    string genreNameSlug = genreName.GenerateSlug();

    var genre = allGenres.FirstOrDefault(x => x.Description.GenerateSlug() == genreNameSlug);
    if (genre is null)
    {
      genre = new Genre(ToTitleCase(genreName));
      allGenres.Add(genre);
    }

    return genre;
  }

  private (string, int?) GetRandomAnswer(string text)
  {
    if (string.IsNullOrEmpty(text))
      return (string.Empty, null);

    var textSplit = text.Split(" ");
    if (!textSplit.Any() || textSplit.Length - 1 < 0)
      return (string.Empty, null);

    var randomIndex = new Random().Next(0, textSplit.Length - 1);

    var answer = string.Join("", textSplit[randomIndex].Trim().Where(c => char.IsLetterOrDigit(c)));
    return (ToTitleCase(answer), randomIndex);
  }

  private TrackLevel ConvertToTrackLevelEnum(string level)
  {
    switch (level.ToLower())
    {
      case "easy": return TrackLevel.Easy;
      case "medium": return TrackLevel.Medium;
      case "hard": return TrackLevel.Hard;
      default:
        return TrackLevel.Easy;
    }
  }

  private string ToTitleCase(string text) =>
    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());

  private SpotifyToken GetSpotifyAccessToken(string clientId, string secretId)
  {
    var url5 = "https://accounts.spotify.com/api/token";

    var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, secretId)));

    var webRequest = (HttpWebRequest)WebRequest.Create(url5);

    webRequest.Method = "POST";
    webRequest.ContentType = "application/x-www-form-urlencoded";
    webRequest.Accept = "application/json";
    webRequest.Headers.Add("Authorization: Basic " + encode_clientid_clientsecret);

    var request = "grant_type=client_credentials";
    var req_bytes = Encoding.ASCII.GetBytes(request);
    webRequest.ContentLength = req_bytes.Length;

    var strm = webRequest.GetRequestStream();
    strm.Write(req_bytes, 0, req_bytes.Length);
    strm.Close();

    var resp = (HttpWebResponse)webRequest.GetResponse();
    var json = "";
    using (var respStr = resp.GetResponseStream())
    {
      using (var rdr = new StreamReader(respStr, Encoding.UTF8))
      {
        json = rdr.ReadToEnd();
        rdr.Close();
      }
    }

    var token = System.Text.Json.JsonSerializer.Deserialize<SpotifyToken>(json);
    return token;
  }

}

public interface ITrackAdapter
{
  Task<PlayList> GetTracks(string playlistName, List<Song> songs);
}

public class SpotifySettings
{
  public string SpotifyClientId { get; set; }
  public string SpotifySecretId { get; set; }
}

public class SpotifyToken
{
  [JsonPropertyName("access_token")]
  public string AccessToken { get; set; }

  [JsonPropertyName("token_type")]
  public string TokenType { get; set; }
}

public class Lyric
{
  [JsonPropertyName("time")]
  public int Time { get; set; }

  [JsonPropertyName("text")]
  public string Text { get; set; }
}

public class Song
{
  [JsonPropertyName("artist")]
  public string Artist { get; set; }

  [JsonPropertyName("lyricsId")]
  public string LyricsId { get; set; }

  [JsonPropertyName("title")]
  public string Title { get; set; }

  private string _lang;
  private string _langCc;
  [JsonPropertyName("lang")]
  public string Lang
  {
    get => _lang;
    set => _lang = value?.ToUpper();
  }

  [JsonPropertyName("LangCc")]
  public string LangCc
  {
    get => _langCc;
    set => _langCc = value?.ToUpper();
  }

  [JsonPropertyName("duration")]
  public int Duration { get; set; }

  [JsonPropertyName("YoutubeVideoId")]
  public string YoutubeVideoId { get; set; }

  [JsonPropertyName("level")]
  public string Level { get; set; }

  [JsonPropertyName("hits")]
  public string Hits { get; set; }

  [JsonPropertyName("lyrics")]
  public List<Lyric> Lyrics { get; set; }
}
