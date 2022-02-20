using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Entities.CourseAggregate;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Enums;
using Langueedu.Core.Features.Commands.Course.CreateCourse;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Services;

public class CourseService : ICourseService
{
  private readonly IRepository<Track> _courseRepository;

  public CourseService(IRepository<Track> courseRepository)
  {
    _courseRepository = courseRepository;
  }

  public async Task<Result<Course>> CreateCourse(CreateCourseCommand createCourse)
  {
    var validator = new CreateCourseCommandValidatior();
    var validate = validator.Validate(createCourse);
    if (!validate.IsValid)
      return Result<Course>.Invalid(validate.AsErrors());

    var track = await _courseRepository.GetByIdAsync(createCourse.TrackId);
    if (track == null)
      return Result<Course>.Error("Track not found!");

    var course = new Course(createCourse.BalanceType,
                            createCourse.CourseLevel,
                            createCourse.CourseMode,
                            createCourse.CourseFee);

    course.AddParticipant(new CourseParticipant(createCourse.UserId, CourseStatus.Created));

    track.AddCourse(course);

    await _courseRepository.UpdateAsync(track);

    await _courseRepository.SaveChangesAsync();

    return course;
  }
}

