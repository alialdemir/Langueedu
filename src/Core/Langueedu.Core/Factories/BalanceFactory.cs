using Langueedu.Core.Enums;

namespace Langueedu.Core.Factories;

public static class BalanceFactory
{
  public static Entities.BalanceAggregate.Balance? Create(BalanceTypes balanceType, params object[] args)
  {
    var balance = typeof(Entities.BalanceAggregate.Balance)
           .Assembly.GetTypes()
           .Where(x => x.IsSubclassOf(typeof(Entities.BalanceAggregate.Balance)))
           .Select(x => (Entities.BalanceAggregate.Balance?)Activator.CreateInstance(x, args))
           .FirstOrDefault(x => x.GetType().Name.EndsWith(balanceType.ToString()));

    return balance;
  }
}

