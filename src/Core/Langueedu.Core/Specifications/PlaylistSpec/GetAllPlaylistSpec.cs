using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel.ViewModels;
using Langueedu.SharedKernel.ViewModels.Playlist;

namespace Langueedu.Core.Specifications;

public class GetAllPlaylistSpec : Specification<PlayList, PlaylistViewModel>
{
  public GetAllPlaylistSpec()
  {
    Query
      .Where(x => x.ContentStatus == ContentStatus.Active);

    Query
      .Include(x =>
      x.Tracks
      .Where(x => x.Album.ContentStatus == ContentStatus.Active)
      .OrderByDescending(x => x.DisplayOrder)
      )
      .ThenInclude(x => x.PerformsOnSongs)
      .ThenInclude(x => x.Artist.Images);

    Query
    .Select(playlist => new PlaylistViewModel
    {
      Name = playlist.Name,
      Tracks = playlist
      .Tracks
      .Select(track => new TrackViewModel
      {
        Id = track.Id,
        Slug = track.Slug,
        Name = track.Name,
        Images = track.Images,

        Album = new AlbumViewModel
        {
          Name = track.Album.Name,
          Slug = track.Album.Slug,
        },

        Artists = track
         .PerformsOnSongs
         .Where(x => x.Artist.ContentStatus == ContentStatus.Active)
         .Select(p => new ArtistViewModel
         {
           Id = p.Artist.Id,
           Name = p.Artist.Name,
           Slug = p.Artist.Slug,
           Images = p.Artist.Images.Select(x => new ImageViewModel
           {
             Url = x.Url,
             Height = x.Height,
             Width = x.Width
           }),
         })
      })
    })
    .OrderByDescending(x => x.DisplayOrder);
  }
}
