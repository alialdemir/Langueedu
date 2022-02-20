using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Features.Commands.Balance.BalanceDecrease;
using Langueedu.Core.Specifications.Balance;
using Langueedu.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class BalanceDecreaseCommandHandlerHandler
{
  private readonly Mock<IRepository<Balance>> _courseService = new();
  private readonly Mock<ILogger<BalanceDecreaseCommandHandler>> _logger = new();

  [Fact]
  public async Task GivenBalanceGolThenDecreaseGold()
  {
    BalanceGold balanceGold = new BalanceGold(Constants.UserId);
    balanceGold.Increase(balanceGold, 1000);

    _courseService
      .Setup(x => x.GetBySpecAsync(It.IsAny<GetBalanceByUserIdSpec>(), default).Result)
      .Returns(Result<Balance>.Success(balanceGold));

    var handler = new BalanceDecreaseCommandHandler(_courseService.Object, _logger.Object);

    var command = new BalanceDecreaseCommand(balanceGold, 100);

    await handler.Handle(command, CancellationToken.None);

    Assert.Equal(900, balanceGold.Gold);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNegativeAmount()
  {
    decimal negativeAmount = -100;

    BalanceDecreaseCommand command = new BalanceDecreaseCommand(new Balance(Constants.UserId), negativeAmount);
    var handler = new BalanceDecreaseCommandHandler(_courseService.Object, _logger.Object);

#nullable disable
    Exception ex = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task GivenBalanceGolThenDecreaseSilver()
  {
    BalanceSilver balanceSilver = new BalanceSilver(Constants.UserId);
    balanceSilver.Increase(balanceSilver, 300);

    _courseService
      .Setup(x => x.GetBySpecAsync(It.IsAny<GetBalanceByUserIdSpec>(), default).Result)
      .Returns(Result<Balance>.Success(balanceSilver));

    var handler = new BalanceDecreaseCommandHandler(_courseService.Object, _logger.Object);

    var command = new BalanceDecreaseCommand(balanceSilver, 100);

    await handler.Handle(command, CancellationToken.None);

    Assert.Equal(200, balanceSilver.Silver);
  }
}

