using System.ComponentModel.DataAnnotations;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Langueedu.Core.Features.Commands.Balance.BalanceIncrease;

public class BalanceIncreaseCommandHandler : INotificationHandler<BalanceIncreaseCommand>
{
  private readonly IRepository<Entities.BalanceAggregate.Balance> _balanceRepository;
  private readonly ILogger<BalanceIncreaseCommandHandler> _logger;
  private readonly IMediator _mediator;

  public BalanceIncreaseCommandHandler(IRepository<Entities.BalanceAggregate.Balance> balanceRepository,
                                       ILogger<BalanceIncreaseCommandHandler> logger,
                                       IMediator mediator)
  {
    _balanceRepository = balanceRepository;
    _logger = logger;
    _mediator = mediator;
  }

  public async Task Handle(BalanceIncreaseCommand request, CancellationToken cancellationToken)
  {
    var validator = new BalanceIncreaseCommandValidator();
    var validate = validator.Validate(request);
    if (!validate.IsValid)
    {
      var error = validate?.Errors?.FirstOrDefault();
      throw new ValidationException(error?.ErrorMessage);
    }

    var balance = await _mediator.Send(new GetBalanceByUserIdQuery(request.Balance.UserId));

    balance = request.Balance.Increase(balance, request.Amount);

    await _balanceRepository.UpdateAsync(balance);

    // TODO: It should be handled again against error scenarios.
    await _balanceRepository.SaveChangesAsync();

    _logger.LogInformation("The balance increased has occurred.", balance);
  }
}

