using Ardalis.Specification;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel.ViewModels;
using Langueedu.SharedKernel.ViewModels.Playlist;

namespace Langueedu.Core.Specifications;

public class GetTrackDetailByTrackIdSpec : Specification<Entities.PlaylistAggregate.Track, TrackDetailViewModel>
{
  public GetTrackDetailByTrackIdSpec(int trackId, string userId)
  {
    Query
      .Where(x => x.Id == trackId &&
      x.ContentStatus == ContentStatus.Active &&
      x.Album.ContentStatus == ContentStatus.Active &&
      x.PerformsOnSongs.Any(x => x.Artist.ContentStatus == ContentStatus.Active));

    Query
    .Select(track => new TrackDetailViewModel
    {
      Id = track.Id,
      YoutubeVideoId = track.YoutubeVideoId,
      Name = track.Name,
      Slug = track.Slug,
      IsFollowed = track.FollowerTracks.Any(x => x.UserId == userId && x.TrackId == trackId),
      FollowerCount = track.FollowerCount,
      Images = track.Images,
      Album = new AlbumViewModel
      {
        Name = track.Album.Name,
        Slug = track.Album.Slug,
        Images = track.Album.Images.Select(image => new ImageViewModel
        {
          Url = image.Url,
          Width = image.Width,
          Height = image.Height
        })
      },
      Artists = track.PerformsOnSongs.Select(artist => new ArtistViewModel
      {
        Id = artist.Id,
        Slug = artist.Artist.Slug,
        Name = artist.Artist.Name,
        Images = artist.Artist.Images.Select(image => new ImageViewModel
        {
          Url = image.Url,
          Width = image.Width,
          Height = image.Height
        })
      }),
    });
  }
}
