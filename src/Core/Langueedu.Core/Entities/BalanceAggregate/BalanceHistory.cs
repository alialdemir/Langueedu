using Langueedu.Core.Enums;
using Langueedu.SharedKernel;

namespace Langueedu.Core.Entities.BalanceAggregate;

public class BalanceHistory : BaseEntity
{
  public BalanceHistory(string userId)
  {
    UserId = userId;
  }

  public BalanceHistoryTypes BalanceHistoryType { get; private set; }

  public BalanceTypes BalanceType { get; private set; }

  public decimal OldAmount { get; private set; }

  public decimal NewAmount { get; private set; }

  public string UserId { get; private set; }


  public BalanceHistory ChanegBalanceHistoryType(BalanceHistoryTypes balanceHistoryType)
  {
    BalanceHistoryType = balanceHistoryType;

    return this;
  }

  public BalanceHistory ChanegBalanceHistoryType(BalanceTypes balanceType)
  {
    BalanceType = balanceType;

    return this;
  }

  public BalanceHistory AddOldAmount(decimal amount)
  {
    OldAmount = amount;

    return this;
  }

  public BalanceHistory AddNewAmount(decimal amount)
  {
    NewAmount = amount;

    return this;
  }
}

