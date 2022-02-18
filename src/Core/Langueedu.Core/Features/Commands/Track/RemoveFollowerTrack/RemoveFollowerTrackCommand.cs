using Ardalis.Result;
using MediatR;

namespace Langueedu.Core.Features.Commands.Playlist.RemoveFollowerTrack;

public class RemoveFollowerTrackCommand : IRequest<Result<bool>>
{
  public RemoveFollowerTrackCommand(string userId, short trackId)
  {
    UserId = userId;
    TrackId = trackId;
  }

  public string UserId { get; }
  public short TrackId { get; set; }
}

