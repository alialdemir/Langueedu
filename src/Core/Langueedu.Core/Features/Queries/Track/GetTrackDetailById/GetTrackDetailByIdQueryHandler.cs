using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.ViewModels;
using MediatR;

namespace Langueedu.Core.Features.Queries.Track.GetTrackDetailById;

public class GetTrackDetailByIdQueryHandler : IRequestHandler<GetTrackDetailByIdQuery, Result<TrackDetailViewModel>>
{
  private readonly ITrackService _trackService;

  public GetTrackDetailByIdQueryHandler(ITrackService trackService)
  {
    _trackService = trackService;
  }

  public async Task<Result<TrackDetailViewModel>> Handle(GetTrackDetailByIdQuery request, CancellationToken cancellationToken)
  {
    var validator = new GetTrackDetailByIdQueryValidatior();
    var validate = validator.Validate(request);
    if (!validate.IsValid)
    {
      return Result<TrackDetailViewModel>.Invalid(validate.AsErrors());
    }

    var response = await _trackService
        .GetTrackDetailByIdAsync(request.TrackId, request.UserId);

    return response;
  }
}

