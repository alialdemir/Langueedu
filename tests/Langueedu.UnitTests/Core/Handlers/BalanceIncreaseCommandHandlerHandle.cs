using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using AutoMapper;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Enums;
using Langueedu.Core.Features.Commands.Balance.BalanceIncrease;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Specifications.Balance;
using Langueedu.SharedKernel.Interfaces;
using MediatR;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class BalanceIncreaseCommandHandlerHandle
{
  private readonly Mock<IRepository<Balance>> _balanceService = new();
  private readonly Mock<IAppLogger<BalanceIncreaseCommandHandler>> _logger = new();
  private readonly Mock<IMediator> _mediator = new();
  private readonly Mock<IMapper> _mapper = new();
  private readonly Balance _balance = new(Constants.UserId);

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

    _mapper
          .Setup(x => x.Map(It.IsAny<Balance>(), It.IsAny<Balance>()))
          .Returns(balanceGold);

    var handler = new BalanceIncreaseCommandHandler(_balanceService.Object,
                                                    _logger.Object,
                                                    _mediator.Object,
                                                    _mapper.Object);

    var command = new BalanceIncreaseCommand(Constants.UserId, BalanceTypes.Gold, 100);

    await handler.Handle(command, CancellationToken.None);

    _balanceService.Verify(sender => sender.UpdateAsync(balanceGold, default));

    Assert.Equal(100, balanceGold.Gold);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNegativeAmount()
  {
    decimal negativeAmount = -100;

    IMapper mapper = Mock.Of<IMapper>();

    BalanceIncreaseCommand command = new BalanceIncreaseCommand(Constants.UserId, BalanceTypes.Gold, negativeAmount);
    var handler = new BalanceIncreaseCommandHandler(_balanceService.Object, _logger.Object, _mediator.Object, mapper);

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

    _mapper
          .Setup(x => x.Map(It.IsAny<Balance>(), It.IsAny<Balance>()))
          .Returns(balanceSilver);

    var handler = new BalanceIncreaseCommandHandler(_balanceService.Object,
                                                    _logger.Object,
                                                    _mediator.Object,
                                                    _mapper.Object);

    var command = new BalanceIncreaseCommand(Constants.UserId, BalanceTypes.Silver, 100);

    await handler.Handle(command, CancellationToken.None);

    _balanceService.Verify(sender => sender.UpdateAsync(balanceSilver, default));

    Assert.Equal(100, balanceSilver.Silver);
  }
}

