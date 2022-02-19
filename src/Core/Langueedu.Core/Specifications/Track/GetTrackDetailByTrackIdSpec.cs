using Ardalis.Specification;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel.ViewModels;

namespace Langueedu.Core.Specifications;

public class GetTrackDetailByTrackIdSpec : Specification<Entities.PlaylistAggregate.Track, TrackDetailViewModel>
{
  public GetTrackDetailByTrackIdSpec(int trackId, string userId)
  {
    Query
      .Where(x => x.Id == trackId && x.ContentStatus == ContentStatus.Active);

    Query
    .Select(track => new TrackDetailViewModel
    {
      Id = track.Id,
      YoutubeVideoId = track.YoutubeVideoId,
      SongTitle = track.SongTitle,
      PicturePath = track.PicturePath,
      Slug = track.Slug,
      AlbumName = track.Album.Name,
      AlbumSlug = track.Album.Slug,
      IsFollowed = track.FollowerTracks.Any(x => x.UserId == userId && x.TrackId == trackId),
      FollowerCount = track.FollowerCount,
      MainArtistId = track.Album.MainArtist.Id,
      Artists = track.PerformsOnSongs.Select(artist => new ArtistViewModel
      {
        Id = artist.Id,
        Slug = artist.Artist.Slug,
        Name = artist.Artist.Name
      }),
    });
  }
}
