using Ardalis.Specification;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel.ViewModels;

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
      SongTitle = track.Name,
      //PicturePath = track.PicturePath,
      Slug = track.Slug,
      AlbumName = track.Album.Name,
      AlbumSlug = track.Album.Slug,
      IsFollowed = track.FollowerTracks.Any(x => x.UserId == userId && x.TrackId == trackId),
      FollowerCount = track.FollowerCount,
      Artists = track.PerformsOnSongs.Select(artist => new ArtistViewModel
      {
        Id = artist.Id,
        Slug = artist.Artist.Slug,
        Name = artist.Artist.Name
      }),
    });
  }
}
