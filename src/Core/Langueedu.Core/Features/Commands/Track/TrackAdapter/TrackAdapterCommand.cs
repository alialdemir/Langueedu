using MediatR;
using Microsoft.AspNetCore.Http;

namespace Langueedu.Core.Features.Commands.Track.TrackAdapter;

public class TrackAdapterCommand : INotification
{
  public TrackAdapterCommand(IFormFile file)
  {
    File = file;
  }

  public IFormFile File { get; }
}

