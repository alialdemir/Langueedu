using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Features.Queries.BalanceQuesries;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Specifications.Balance;
using Langueedu.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class GetBalanceByUserIdQueryHandlerHandle
{
  private readonly GetBalanceByUserIdQueryHandler _handler;
  private readonly Mock<IReadRepository<Balance>> _balanceReadRepository = new();
  private readonly Mock<IRepository<Balance>> _balanceRepository = new();
  private readonly BalanceGold _balance = new(Constants.UserId);

  public GetBalanceByUserIdQueryHandlerHandle()
  {
    _balanceReadRepository.Setup(foo => foo.GetBySpecAsync(It.IsAny<GetBalanceByUserIdSpec>(), default).Result).Returns(_balance);

    _handler = new GetBalanceByUserIdQueryHandler(_balanceReadRepository.Object, _balanceRepository.Object);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNullEventArgument()
  {
#nullable disable
    Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task SendUserIdGivenReturnGolds()
  {
    var result = await _handler.Handle(new GetBalanceByUserIdQuery(Constants.UserId), CancellationToken.None);

    Assert.True(result.IsSuccess);
    Assert.Equal(_balance.Gold, result.Value.Gold);
  }

  [Fact]
  public async Task SendNullUserIdGivenValidationErrors()
  {
    var result = await _handler.Handle(new GetBalanceByUserIdQuery(null), CancellationToken.None);

    Assert.False(result.IsSuccess);
    Assert.Null(result.Value);
    Assert.Contains(result.ValidationErrors, x => x.ErrorMessage == "You must enter a user id!");
    Assert.Contains(result.ValidationErrors, x => x.Identifier == "UserId");
  }
}

