using System.Net.Http;
using System.Threading.Tasks;
using Langueedu.Sdk.Course.Request;
using Langueedu.Sdk.Course.Response;
using Langueedu.Sdk.Interfaces;
using Langueedu.Sdk.Utilities;

namespace Langueedu.Sdk.Course
{
  public class CourseService : ServiceBase, ICourseService
  {
    private const string API_URL_BASE = "/v1/Courses";

    public CourseService(IHttpClientFactory clientFactory,
                         IToastService toastService)
        : base(clientFactory.CreateClient("LangueeduApi"), toastService)
    {
    }

    public async Task<CourseDetailViewModel> CreateCourse(CreateCourseModel createCourse)
    {
      if (createCourse.TrackId <= 0)
        return Result<CourseDetailViewModel>.Invalid("Track id cannot be zero or less than zero.", nameof(createCourse.TrackId));

      string uri = UriHelper.CombineUri(Configs.GatewaEndpoint, API_URL_BASE);

      var response = await PostAsync<CourseDetailViewModel>(uri, createCourse);
      return response?.Value;
    }
  }
}