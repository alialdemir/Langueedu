﻿using System.Collections.Generic;

namespace Langueedu.Sdk.Playlist.Response
{
  public class ArtistViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public IEnumerable<ImageViewModel> Images { get; set; }
  }
}