﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Langueedu.API.Controllers;

/// <summary>
/// If your API controllers will use a consistent route convention and the [ApiController] attribute (they should)
/// then it's a good idea to define and use a common base controller class like this one.
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : Controller
{
  private string userId = String.Empty;

  public string UserId
  {
    get
    {
      if (String.IsNullOrEmpty(userId))
      {
        userId = User.FindFirst("sub")?.Value;
      }

      return userId;
    }
  }
}
