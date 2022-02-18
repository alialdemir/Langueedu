using Langueedu.Core.Features.Commands.Account.SignUp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Langueedu.API.Controllers;

[ApiVersion("1.0")]
public class AccountsController : BaseApiController
{
  private readonly IMediator _mediator;

  public AccountsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [SwaggerOperation(
      Summary = "Sign up a user",
      Description = "Sign up a user",
      OperationId = "Accounts.SignInAsnc",
      Tags = new[] { "Sign up Endpoints" })
  ]
  [HttpPost]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> SignUpAsnc([FromBody] SignUpCommand signUpModel)
  {
    var signUp = await _mediator.Send(signUpModel);

    return signUp.ToActionResult();
  }
}
