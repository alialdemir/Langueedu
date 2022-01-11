using System;
using Ardalis.Specification;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel.ViewModels;

namespace Langueedu.Core.Specifications;

public class GetTrackDetailByTrackIdSpec : Specification<Track, TrackDetailViewModel>
{
    public GetTrackDetailByTrackIdSpec(int trackId)
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
            Artists = track.PerformsOnSongs.Select(artist => new ArtistViewModel
            {
                Slug = artist.Artist.Slug,
                Name = artist.Artist.Name
            }),
        });
    }
}
