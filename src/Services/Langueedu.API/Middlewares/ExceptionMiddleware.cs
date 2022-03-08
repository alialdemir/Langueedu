using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Ardalis.Result;

namespace Langueedu.API.Middlewares;

public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;

  public ExceptionMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      await _next(httpContext);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(httpContext, ex);
    }
  }

  private async Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";

    if (exception is ValidationException validationException)
    {
      Result<bool> result = Result<bool>.Error(validationException.Message);
      context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
      await context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }

    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    Result<bool> exceptionGlobal = Result<bool>.Error(exception.Message ?? exception.StackTrace);
    await context.Response.WriteAsync(JsonSerializer.Serialize(exceptionGlobal));
  }
}
