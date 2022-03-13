using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.Result;
using Langueedu.Core.Features.Commands.Account.SignIn;
using Langueedu.Core.Features.Commands.Account.SignUp;
using Langueedu.SharedKernel.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
      OperationId = "Accounts.SignUp",
      Tags = new[] { "Account Endpoints" })
  ]
  [HttpPost]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> SignUpAsnc([FromBody] SignUpCommand signUpModel)
  {
    var signUp = await _mediator.Send(signUpModel);

    return signUp.ToActionResult();
  }

  [SwaggerOperation(
      Summary = "Token",
      Description = "Get Token",
      OperationId = "Accounts.SignIn",
      Tags = new[] { "Account Endpoints" })
  ]
  [AllowAnonymous]
  [HttpPost("Token")]
  [ProducesResponseType(typeof(Result<TokenViewModel>), StatusCodes.Status200OK)]
  public async Task<IActionResult> SignIn([FromBody] SignInCommand signInCommand)
  {
    var signIn = await _mediator.Send(signInCommand);

    return signIn.ToActionResult();
  }
}
