using System.Net;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Langueedu.Web.Controllers;

/// <summary>
/// If your API controllers will use a consistent route convention and the [ApiController] attribute (they should)
/// then it's a good idea to define and use a common base controller class like this one.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : Controller
{

}

