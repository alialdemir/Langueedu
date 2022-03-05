using Langueedu.Sdk.Enums;

namespace Langueedu.Sdk.Course.Request
{
  public class CreateCourseModel
  {
    public decimal CourseFee { get; set; }
    public BalanceTypes BalanceType { get; set; }
    public short TrackId { get; set; }
    public CourseModes CourseMode { get; set; }
    public CourseLevel CourseLevel { get; set; }
  }
}
