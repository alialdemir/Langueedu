using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels;
using MediatR;

namespace Langueedu.Core.Features.Queries.Track.GetTrackDetailById;

public class GetTrackDetailByIdQuery : IRequest<Result<TrackDetailViewModel>>
{
  public GetTrackDetailByIdQuery(short trackId, string userId)
  {
    TrackId = trackId;
    UserId = userId;
  }
  public short TrackId { get; set; }
  public string UserId { get; }
}

