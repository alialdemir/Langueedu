using System.Net;
using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Langueedu.API.Controllers;

/// <summary>
/// If your API controllers will use a consistent route convention and the [ApiController] attribute (they should)
/// then it's a good idea to define and use a common base controller class like this one.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : Controller
{

}

