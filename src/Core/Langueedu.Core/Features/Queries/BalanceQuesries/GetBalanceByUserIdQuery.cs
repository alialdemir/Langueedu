using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels.Balance;
using MediatR;

namespace Langueedu.Core.Features.Queries.BalanceQuesries;

public class GetBalanceByUserIdQuery : IRequest<Result<BalanceViewModel>>
{
  public GetBalanceByUserIdQuery(string userId)
  {
    UserId = userId;
  }

  public string UserId { get; }
}