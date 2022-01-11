using FluentValidation;
using Langueedu.Core.Features.Queries.Playlist;

namespace Langueedu.Core.Validations
{
    public class GetAllPlaylistCommandValidator : AbstractValidator<GetAllPlaylistQuery>
    {
        public GetAllPlaylistCommandValidator()
        {
            // RuleFor(p => p.CategoryName)
            //     .NotNull()
            //     .WithMessage("Lütfen 'Name'i boş geçmeyiniz.")
            //     .MaximumLength(20)
            //     .MinimumLength(3)
            //     .WithMessage("'Name' değeri 3 ile 20 karakter arasında olmalıdır.");
        }
    }
}

