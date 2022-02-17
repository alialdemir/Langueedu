using FluentValidation;
using Langueedu.Core.Features.Commands.Playlist.FollowerTrack;

namespace Langueedu.Core.Validations;

public class AddFollowerTrackCommandHandlerValidator : AbstractValidator<AddFollowerTrackCommand>
{
  public AddFollowerTrackCommandHandlerValidator()
  {
    RuleFor(p => p.TrackId)
        .GreaterThan(short.MinValue)
        .WithMessage("Track id must be greater than zero.");

    RuleFor(x => x.UserId)
    .NotEmpty()
    .WithMessage("You must enter a username");
  }
}

