using System;
using FluentValidation;
using FluentValidation.Validators;
using Langueedu.Core.Features.Commands.Account.SignUp;

namespace Langueedu.Core.Validations
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("You must enter a username")
            .MinimumLength(3)
            .WithMessage("Username must be a minimum of 3 characters ");

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("You must enter a email")
            .MinimumLength(3)
            .WithMessage("Email must be a minimum of 3 characters")
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("You must enter a password")
            .MinimumLength(8)
            .WithMessage("Username must be a minimum of 8 characters ");

            RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("You must enter a password")
            .MinimumLength(8)
            .WithMessage("Username must be a minimum of 8 characters ");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match");
                }
            });

            RuleFor(x => x.LanguageCode)
            .NotEmpty()
            .WithMessage("You must enter a language code");
        }
    }
}

