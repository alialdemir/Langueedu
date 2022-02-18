using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Validations;
using MediatR;

namespace Langueedu.Core.Features.Commands.Playlist.FollowerTrack;

public class AddFollowerTrackCommandHandler : IRequestHandler<AddFollowerTrackCommand, Result<bool>>
{
  private readonly IFollowerTrackService _followerTrackService;
  private readonly IMapper _mapper;

  public AddFollowerTrackCommandHandler(IFollowerTrackService followerTrackService,
                                        IMapper mapper)
  {
    _followerTrackService = followerTrackService;
    _mapper = mapper;
  }

  public async Task<Result<bool>> Handle(AddFollowerTrackCommand request, CancellationToken cancellationToken)
  {
    var validator = new AddFollowerTrackCommandHandlerValidator();
    var validate = validator.Validate(request);
    if (!validate.IsValid)
      return Result<bool>.Invalid(validate.AsErrors());

    var followerTrack = _mapper.Map<Entities.PlaylistAggregate.FollowerTrack>(request);

    return await _followerTrackService.AddAsync(followerTrack);
  }
}
