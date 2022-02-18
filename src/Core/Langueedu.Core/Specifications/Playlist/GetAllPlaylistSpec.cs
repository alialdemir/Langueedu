using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel.ViewModels;

namespace Langueedu.Core.Specifications;

public class GetAllPlaylistSpec : Specification<Playlist, PlaylistViewModel>
{
  public GetAllPlaylistSpec()
  {
    Query
      .Where(x => x.ContentStatus == ContentStatus.Active);

    Query
    .Select(playlist => new PlaylistViewModel
    {
      PlaylistId = playlist.Id,
      PlaylistName = playlist.PlaylistName,
      Tracks = playlist
      .Tracks
      .OrderByDescending(x => x.DisplayOrder)
      .Where(t => t.ContentStatus == ContentStatus.Active &&
                  t.Album.ContentStatus == ContentStatus.Active &&
                  t.Album.MainArtist.ContentStatus == ContentStatus.Active)
      .Select(track => new TrackViewModel
      {
        TrackId = track.Id,
        YoutubeId = track.YoutubeVideoId,
        TrackSlug = track.Slug,
        SongTitle = track.SongTitle,
        TrackImage = track.PicturePath,

        AlbumSlug = track.Album.Slug,
        AlbumnName = track.Album.Name,

        MainArtistId = track.Album.MainArtist.Id,

        Artists = track
         .PerformsOnSongs
         .Select(p => new ArtistViewModel
         {
           Id = p.Artist.Id,
           Slug = p.Artist.Slug,
           Image = p.Artist.PicturePath,
           Name = p.Artist.Name,
         }).ToList()
      })
      .ToList()
    })
    .OrderByDescending(x => x.DisplayOrder);
  }
}

