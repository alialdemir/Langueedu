using Ardalis.Result;
using Langueedu.Core.Enums;
using Langueedu.SharedKernel.ViewModels.Course;
using MediatR;
using Newtonsoft.Json;

namespace Langueedu.Core.Features.Commands.Course.CreateCourse;

public class CreateCourseCommand : IRequest<Result<CourseDetailViewModel>>
{
  public CreateCourseCommand(BalanceTypes balanceType,
                             string userId,
                             short trackId,
                             CourseModes courseMode,
                             CourseLevel courseLevel,
                             decimal courseFee)
  {
    BalanceType = balanceType;
    UserId = userId;
    TrackId = trackId;
    CourseMode = courseMode;
    CourseLevel = courseLevel;
    CourseFee = courseFee;
  }
  public decimal CourseFee { get; }
  public BalanceTypes BalanceType { get; }
  [JsonIgnore]
  public string UserId { get; set; }
  public short TrackId { get; set; }
  public CourseModes CourseMode { get; }
  public CourseLevel CourseLevel { get; }
}
