using AutoMapper;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.SharedKernel.ViewModels.Balance;

namespace Langueedu.Core.Mapping;

public class BalanceProfile : Profile
{
  public BalanceProfile()
  {
    CreateMap<BalanceViewModel, Balance>()
      .ReverseMap();
  }
}

