using Ardalis.Result;
using MediatR;

namespace Langueedu.Core.Features.Commands.Playlist.AddFollowerTrack;

public class AddFollowerTrackCommand : IRequest<Result<bool>>
{
  public AddFollowerTrackCommand(string userId, short trackId)
  {
    UserId = userId;
    TrackId = trackId;
  }

  public string UserId { get; }
  public short TrackId { get; set; }
}

