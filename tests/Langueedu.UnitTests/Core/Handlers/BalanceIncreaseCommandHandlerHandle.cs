using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Features.Commands.Balance.BalanceIncrease;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Specifications.Balance;
using Langueedu.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class BalanceIncreaseCommandHandlerHandle
{
  private readonly Mock<IRepository<Balance>> _balanceService = new();
  private readonly Mock<ILogger<BalanceIncreaseCommandHandler>> _logger = new();
  private readonly Mock<IMediator> _mediator = new();

  [Fact]
  public async Task GivenBalanceGoldThenIncreaseGold()
  {
    BalanceGold balanceGold = new BalanceGold(Constants.UserId);

    _balanceService
      .Setup(x => x.GetBySpecAsync(It.IsAny<GetBalanceByUserIdSpec>(), default).Result)
      .Returns(Result<Balance>.Success(balanceGold));

    _mediator
      .Setup(x => x.Send(It.IsAny<GetBalanceByUserIdQuery>(), default).Result)
      .Returns(balanceGold);

    var handler = new BalanceIncreaseCommandHandler(_balanceService.Object, _logger.Object, _mediator.Object);

    var command = new BalanceIncreaseCommand(balanceGold, 100);

    await handler.Handle(command, CancellationToken.None);

    _balanceService.Verify(sender => sender.UpdateAsync(balanceGold, default));

    Assert.Equal(100, balanceGold.Gold);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNegativeAmount()
  {
    decimal negativeAmount = -100;

    BalanceIncreaseCommand command = new BalanceIncreaseCommand(new Balance(Constants.UserId), negativeAmount);
    var handler = new BalanceIncreaseCommandHandler(_balanceService.Object, _logger.Object, _mediator.Object);

#nullable disable
    Exception ex = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task GivenBalanceGoldThenIncreaseSilver()
  {
    BalanceSilver balanceSilver = new BalanceSilver(Constants.UserId);

    _balanceService
      .Setup(x => x.GetBySpecAsync(It.IsAny<GetBalanceByUserIdSpec>(), default).Result)
      .Returns(Result<Balance>.Success(balanceSilver));

    _mediator
      .Setup(x => x.Send(It.IsAny<GetBalanceByUserIdQuery>(), default).Result)
      .Returns(balanceSilver);

    var handler = new BalanceIncreaseCommandHandler(_balanceService.Object, _logger.Object, _mediator.Object);

    var command = new BalanceIncreaseCommand(balanceSilver, 100);

    await handler.Handle(command, CancellationToken.None);

    _balanceService.Verify(sender => sender.UpdateAsync(balanceSilver, default));

    Assert.Equal(100, balanceSilver.Silver);
  }
}

