using MediatR;

namespace Langueedu.Core.Features.Commands.Balance.BalanceIncrease;

public class BalanceIncreaseCommand : INotification
{
  public BalanceIncreaseCommand(Entities.BalanceAggregate.Balance balance, decimal amount)
  {
    Balance = balance;
    Amount = amount;
  }

  public Entities.BalanceAggregate.Balance Balance { get; set; }
  public decimal Amount { get; }
}

