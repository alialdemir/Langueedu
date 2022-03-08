using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Langueedu.Core.Factories;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.Interfaces;
using MediatR;

namespace Langueedu.Core.Features.Commands.Balance.BalanceDecrease;

public class BalanceDecreaseCommandHandler : INotificationHandler<BalanceDecreaseCommand>
{
  private readonly IRepository<Entities.BalanceAggregate.Balance> _balanceRepository;
  private readonly IAppLogger<BalanceDecreaseCommandHandler> _logger;
  private readonly IMediator _mediator;
  private readonly IMapper _mapper;

  public BalanceDecreaseCommandHandler(IRepository<Entities.BalanceAggregate.Balance> balanceRepository,
                                       IAppLogger<BalanceDecreaseCommandHandler> logger,
                                       IMediator mediator,
                                       IMapper mapper)
  {
    _balanceRepository = balanceRepository;
    _logger = logger;
    _mediator = mediator;
    _mapper = mapper;
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

    var balance = (await _mediator.Send(new GetBalanceByUserIdQuery(request.UserId))).Value;

    var balanceByType = BalanceFactory.Create(request.BalanceType, request.UserId);
    if (balanceByType == null)
      throw new ValidationException("The balance type is invalid!");

    balanceByType = _mapper.Map(balance, balanceByType);

    balanceByType.Decrease(request.Amount);

    balance = _mapper.Map(balanceByType, balance);

    await _balanceRepository.UpdateAsync(balance);

    // TODO: It should be handled again against error scenarios.
    await _balanceRepository.SaveChangesAsync();

    _logger.LogInformation("The balance reduction has occurred.", balance);
  }
}

