using System.Threading.Tasks;
using Langueedu.Sdk.Course.Request;
using Langueedu.Sdk.Course.Response;

namespace Langueedu.Sdk.Course
{
  public interface ICourseService
  {
    Task<CourseDetailViewModel> CreateCourse(CreateCourseModel createCourse);
  }
}