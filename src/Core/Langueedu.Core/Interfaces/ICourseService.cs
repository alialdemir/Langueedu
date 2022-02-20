using Ardalis.Result;
using Langueedu.Core.Entities.CourseAggregate;
using Langueedu.Core.Features.Commands.Course.CreateCourse;

namespace Langueedu.Core.Interfaces;

public interface ICourseService
{
  Task<Result<Course>> CreateCourse(CreateCourseCommand course);
}

