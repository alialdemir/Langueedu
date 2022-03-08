using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Enums;

namespace Langueedu.Core.Factories;

public static class BalanceFactory
{
  public static Entities.BalanceAggregate.Balance Create(BalanceTypes balanceType, params object[] args)
  {
    Type iBalanceType = typeof(IBalance);

    var balance = iBalanceType
           .Assembly
           .GetTypes()
           .Where(x => x.GetInterfaces().Contains(iBalanceType))
           .Select(x => (Balance)Activator.CreateInstance(x, args))
           .FirstOrDefault(x => x.GetType().Name.EndsWith(balanceType.ToString()));

    return balance;
  }
}

