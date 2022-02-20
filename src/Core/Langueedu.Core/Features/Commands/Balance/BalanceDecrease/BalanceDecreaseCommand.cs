using MediatR;

namespace Langueedu.Core.Features.Commands.Balance.BalanceDecrease;

public class BalanceDecreaseCommand : INotification
{
  public BalanceDecreaseCommand(Entities.BalanceAggregate.Balance balance, decimal amount)
  {
    Balance = balance;
    Amount = amount;
  }

  public Entities.BalanceAggregate.Balance Balance { get; set; }
  public decimal Amount { get; }
}

