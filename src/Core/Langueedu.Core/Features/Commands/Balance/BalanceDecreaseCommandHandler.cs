using System.ComponentModel.DataAnnotations;
using Langueedu.Core.Specifications.Balance;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Langueedu.Core.Features.Commands.Balance;

public class BalanceDecreaseCommandHandler : INotificationHandler<BalanceDecreaseCommand>
{
  private readonly IRepository<Entities.BalanceAggregate.Balance> _balanceRepository;
  private readonly ILogger<BalanceDecreaseCommandHandler> _logger;

  public BalanceDecreaseCommandHandler(IRepository<Entities.BalanceAggregate.Balance> balanceRepository, ILogger<BalanceDecreaseCommandHandler> logger)
  {
    _balanceRepository = balanceRepository;
    _logger = logger;
  }

  public async Task Handle(BalanceDecreaseCommand request, CancellationToken cancellationToken)
  {
    var validator = new BalanceDecreaseCommandValidator();
    var validate = validator.Validate(request);
    if (!validate.IsValid)
    {
      var error = validate?.Errors?.FirstOrDefault();
      throw new ValidationException(error?.ErrorMessage);
    }

    var spec = new GetBalanceByUserIdSpec(request.Balance.UserId);
    var balance = await _balanceRepository.GetBySpecAsync(spec);
    if (balance == null)
    {
      balance = await _balanceRepository.AddAsync((Entities.BalanceAggregate.Balance)request.Balance, cancellationToken);

      await _balanceRepository.SaveChangesAsync();
    }

    balance = request.Balance.Decrease(balance, request.Amount);

    await _balanceRepository.UpdateAsync(balance);

    // TODO: It should be handled again against error scenarios.
    await _balanceRepository.SaveChangesAsync();

    _logger.LogInformation("The balance reduction has occurred.", balance);
  }
}

