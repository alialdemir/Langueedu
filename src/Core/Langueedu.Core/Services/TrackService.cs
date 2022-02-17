using Ardalis.Result;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Specifications;
using Langueedu.SharedKernel.Interfaces;
using Langueedu.SharedKernel.ViewModels;

namespace Langueedu.Core.Services;

public class TrackService : ITrackService
{
  private readonly IReadRepository<Track> _trackRepository;

  public TrackService(IReadRepository<Track> trackRepository)
  {
    _trackRepository = trackRepository;
  }

  public async Task<Result<TrackDetailViewModel>> GetTrackDetailByIdAsync(int trackId, string userId)
  {
    if (trackId <= 0)
    {
      return Core.Result.Invalid<TrackDetailViewModel>("Track id must be greater than zero.", nameof(trackId));
    }

    var spec = new GetTrackDetailByTrackIdSpec(trackId, userId);
    var response = await _trackRepository
        .GetBySpecAsync(spec);

    return Result<TrackDetailViewModel>.Success(response);
  }
}
