using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Langueedu.Web.Components.Models;

public class LoginModel
{
    public string UserName { get; set; }

    public string Password
    {
        get; set;
    }
}

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(x => x.UserName)
        .NotEmpty()
        .WithMessage("You must enter a username")
        .MinimumLength(3)
        .WithMessage("Username must be a minimum of 3 characters ");

        RuleFor(x => x.Password)
        .NotEmpty()
        .WithMessage("You must enter a password")
        .MinimumLength(8)
        .WithMessage("Username must be a minimum of 8 characters ");

    }

}