﻿using Langueedu.SharedKernel.ViewModels.Playlist;

namespace Langueedu.SharedKernel.ViewModels;

public class TrackDetailViewModel
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Slug { get; set; }
  public IEnumerable<ArtistViewModel> Artists { get; set; }
  public string YoutubeVideoId { get; set; }
  public bool IsFollowed { get; set; }
  public long FollowerCount { get; set; }
  public AlbumViewModel Album { get; set; }
  public IEnumerable<ImageViewModel> Images { get; set; }
}

