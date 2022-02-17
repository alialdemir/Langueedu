using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels;
using MediatR;

namespace Langueedu.Core.Features.Queries.Track.GetTrackDetailById;

public class GetTrackDetailByIdQuery : IRequest<Result<TrackDetailViewModel>>
{
  public GetTrackDetailByIdQuery(short trackId)
  {
    TrackId = trackId;
  }
  public short TrackId { get; set; }
}

