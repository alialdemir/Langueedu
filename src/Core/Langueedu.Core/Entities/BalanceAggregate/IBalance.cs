using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.BalanceAggregate;

public interface IBalance : IAggregateRoot
{
  decimal Gold { get; }

  decimal Silver { get; }
  string UserId { get; }
  Balance Increase(decimal amount);
  Balance Decrease(decimal amount);
}
