using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Image : BaseEntity, IAggregateRoot
{
  public Image(string url, int width, int height)
  {
    Url = url;
    Width = width;
    Height = height;
  }

  public string Url { get; set; }

  public int Width { get; set; }

  public int Height { get; set; }

  public Artist? Artist { get; set; }

  public Album Album { get; set; }
}

