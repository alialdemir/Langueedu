using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Langueedu.API
{
    public static class ResultExtension
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result == null)
            {
                return new BadRequestObjectResult("No such record found.");
            }

            result.ClearValueType();

            switch (result.Status)
            {
                case ResultStatus.Ok: return new OkObjectResult(result);

                case ResultStatus.Forbidden: return new ForbidResult();

                case ResultStatus.Unauthorized: return new UnauthorizedObjectResult(result);

                case ResultStatus.NotFound: return new NoContentResult();

                case ResultStatus.Error:
                case ResultStatus.Invalid:
                    return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(null);
        }
    }
}

