using Ardalis.Result;
using MediatR;

namespace Langueedu.Core.Features.Commands.Playlist.FollowerTrack;

public class AddFollowerTrackCommand : IRequest<Result<string>>
{
  public AddFollowerTrackCommand(string userId, short trackId)
  {
    UserId = userId;
    TrackId = trackId;
  }

  public string UserId { get; }
  public short TrackId { get; set; }
}

