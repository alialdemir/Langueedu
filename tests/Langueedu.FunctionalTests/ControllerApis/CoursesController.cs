using System.Net.Http.Json;
using Ardalis.Result;
using Langueedu.API;
using Langueedu.Core.Enums;
using Langueedu.Core.Features.Commands.Course.CreateCourse;
using Langueedu.SharedKernel.ViewModels.Course;
using Xunit;

namespace Langueedu.FunctionalTests.ControllerApis;

public class CoursesController : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public CoursesController(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsAnyItemsCourseDetail()
  {
    var command = new CreateCourseCommand(BalanceTypes.Gold,
                                          userId: "",
                                          trackId: 1,
                                          CourseModes.GapFilling,
                                          CourseLevel.Advanced,
                                          10);

    var result = await _client.PostAsJsonAsync("/api/v1/Courses", command);
    var courseDetail = await result.Content.ReadFromJsonAsync<Result<CourseDetailViewModel>>();

    Assert.NotNull(courseDetail);
    Assert.NotNull(courseDetail.Value.YoutubeVideoId);
    Assert.NotNull(courseDetail.Value.Lyrics);
    Assert.NotEqual(0, courseDetail.Value.CourseId);
  }
}

