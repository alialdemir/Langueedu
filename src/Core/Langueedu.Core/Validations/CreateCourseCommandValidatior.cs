using FluentValidation;
using Langueedu.Core.Features.Commands.Course.CreateCourse;

namespace Langueedu.Core.Validations;

public class CreateCourseCommandValidatior : AbstractValidator<CreateCourseCommand>
{
  public CreateCourseCommandValidatior()
  {
    RuleFor(p => p.TrackId)
        .GreaterThan(short.MinValue)
        .WithMessage("Track id must be greater than zero.");

    RuleFor(x => x.UserId)
    .NotEmpty()
    .WithMessage("You must enter a username");

    RuleFor(x => x.CourseMode)
    .NotNull()
    .WithMessage("Course mode required field!");

    RuleFor(x => x.CourseLevel)
    .NotNull()
    .WithMessage("Course level required field!");

    RuleFor(x => x.CourseFee)
    .GreaterThan((short)0)
    .WithMessage("The course fee cannot be less than zero!");
  }
}

