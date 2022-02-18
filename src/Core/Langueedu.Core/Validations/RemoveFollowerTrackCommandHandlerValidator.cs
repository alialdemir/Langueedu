using FluentValidation;
using Langueedu.Core.Features.Commands.Playlist.RemoveFollowerTrack;

namespace Langueedu.Core.Validations;

public class RemoveFollowerTrackCommandHandlerValidator : AbstractValidator<RemoveFollowerTrackCommand>
{
  public RemoveFollowerTrackCommandHandlerValidator()
  {
    RuleFor(p => p.TrackId)
        .GreaterThan(short.MinValue)
        .WithMessage("Track id must be greater than zero.");

    RuleFor(x => x.UserId)
    .NotEmpty()
    .WithMessage("You must enter a username");
  }
}

