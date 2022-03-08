using Langueedu.Core.Enums;
using MediatR;

namespace Langueedu.Core.Features.Commands.Balance.BalanceDecrease;

public class BalanceDecreaseCommand : INotification
{
  public BalanceDecreaseCommand(string userId, BalanceTypes balanceType, decimal amount)
  {
    UserId = userId;
    BalanceType = balanceType;
    Amount = amount;
  }

  public string UserId { get; }
  public BalanceTypes BalanceType { get; }
  public decimal Amount { get; }
}

