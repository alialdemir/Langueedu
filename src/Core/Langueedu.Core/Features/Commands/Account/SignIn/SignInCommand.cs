using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels.Account;
using MediatR;

namespace Langueedu.Core.Features.Commands.Account.SignIn;

public class SignInCommand : IRequest<Result<TokenViewModel>>
{
  public string UserName { get; set; }
  public string Password { get; set; }
}