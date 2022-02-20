using Ardalis.Result;
using Langueedu.Core.Features.Commands.Course.CreateCourse;
using Langueedu.SharedKernel.ViewModels.Course;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Langueedu.API.Controllers;

[ApiVersion("1.0")]
public class CoursesController : BaseApiController
{
  private readonly IMediator _mediator;

  public CoursesController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [SwaggerOperation(
      Summary = "Create Course",
      Description = "Create a new course",
      OperationId = "Course.CreateCourse",
      Tags = new[] { "Course Endpoints" })
  ]
  [HttpPost]
  [ProducesResponseType(typeof(Result<CourseDetailViewModel>), StatusCodes.Status200OK)]
  public async Task<IActionResult> FollowTrack([FromBody] CreateCourseCommand course)
  {
    course.UserId = UserId;

    var result = await _mediator.Send(course);

    return result.ToActionResult();
  }

}

