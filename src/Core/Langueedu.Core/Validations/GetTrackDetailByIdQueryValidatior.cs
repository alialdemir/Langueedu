using FluentValidation;
using Langueedu.Core.Features.Queries.Track.GetTrackDetailById;

namespace Langueedu.Core.Validations;

public class GetTrackDetailByIdQueryValidatior : AbstractValidator<GetTrackDetailByIdQuery>
{
  public GetTrackDetailByIdQueryValidatior()
  {
    RuleFor(p => p.TrackId)
        .GreaterThan(short.MinValue)
        .WithMessage("Track id must be greater than zero.");
  }
}

