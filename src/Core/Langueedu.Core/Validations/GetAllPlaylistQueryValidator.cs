using FluentValidation;
using Langueedu.Core.Features.Queries.Playlist.GetAllPlaylist;

namespace Langueedu.Core.Validations;

public class GetAllPlaylistQueryValidator : AbstractValidator<GetAllPlaylistsQuery>
{
  public GetAllPlaylistQueryValidator()
  {
    //RuleFor(p => p.UserId)
    //    .NotNull()
    //    .WithMessage("Please fill in the 'user id' blank!")
    //    .MaximumLength(20)
    //    .MinimumLength(3)
    //    .WithMessage("'User Id' must be between 3 and 20 characters.");
  }
}

