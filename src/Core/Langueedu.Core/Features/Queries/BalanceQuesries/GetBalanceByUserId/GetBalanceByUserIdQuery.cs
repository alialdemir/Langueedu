using Ardalis.Result;
using Langueedu.Core.Entities.BalanceAggregate;
using MediatR;

namespace Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;

public class GetBalanceByUserIdQuery : IRequest<Result<Balance>>
{
  public GetBalanceByUserIdQuery(string userId)
  {
    UserId = userId;
  }

  public string UserId { get; }
}