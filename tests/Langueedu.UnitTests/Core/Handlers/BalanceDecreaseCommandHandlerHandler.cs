using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Features.Commands.Balance.BalanceDecrease;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Specifications.Balance;
using Langueedu.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class BalanceDecreaseCommandHandlerHandler
{
  private readonly Mock<IRepository<Balance>> _balanceService = new();
  private readonly Mock<IAppLogger<BalanceDecreaseCommandHandler>> _logger = new();
  private readonly Mock<IMediator> _mediator = new();

  [Fact]
  public async Task GivenBalanceGoldThenDecreaseGold()
  {
    BalanceGold balanceGold = new BalanceGold(Constants.UserId);
    balanceGold.Increase(balanceGold, 1000);

    _balanceService
      .Setup(x => x.GetBySpecAsync(It.IsAny<GetBalanceByUserIdSpec>(), default).Result)
      .Returns(Result<Balance>.Success(balanceGold));

    _mediator
      .Setup(x => x.Send(It.IsAny<GetBalanceByUserIdQuery>(), default).Result)
      .Returns(balanceGold);

    var handler = new BalanceDecreaseCommandHandler(_balanceService.Object, _logger.Object, _mediator.Object);

    var command = new BalanceDecreaseCommand(balanceGold, 100);

    await handler.Handle(command, CancellationToken.None);

    _balanceService.Verify(sender => sender.UpdateAsync(balanceGold, default));

    Assert.Equal(900, balanceGold.Gold);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNegativeAmount()
  {
    decimal negativeAmount = -100;

    BalanceDecreaseCommand command = new BalanceDecreaseCommand(new Balance(Constants.UserId), negativeAmount);
    var handler = new BalanceDecreaseCommandHandler(_balanceService.Object, _logger.Object, _mediator.Object);

#nullable disable
    Exception ex = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task GivenBalanceGoldThenDecreaseSilver()
  {
    BalanceSilver balanceSilver = new BalanceSilver(Constants.UserId);
    balanceSilver.Increase(balanceSilver, 300);


    _balanceService
      .Setup(x => x.GetBySpecAsync(It.IsAny<GetBalanceByUserIdSpec>(), default).Result)
      .Returns(Result<Balance>.Success(balanceSilver));

    _mediator
      .Setup(x => x.Send(It.IsAny<GetBalanceByUserIdQuery>(), default).Result)
      .Returns(balanceSilver);

    var handler = new BalanceDecreaseCommandHandler(_balanceService.Object, _logger.Object, _mediator.Object);

    var command = new BalanceDecreaseCommand(balanceSilver, 100);

    await handler.Handle(command, CancellationToken.None);

    _balanceService.Verify(sender => sender.UpdateAsync(balanceSilver, default));

    Assert.Equal(200, balanceSilver.Silver);
  }
}

