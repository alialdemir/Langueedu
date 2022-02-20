using Ardalis.Result;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Specifications.Lyrics;
using Langueedu.SharedKernel.Interfaces;
using Langueedu.SharedKernel.ViewModels.Course;

namespace Langueedu.Core.Services;

public class LyricsService : ILyricsService
{
  private readonly IReadRepository<Lyrics> _lyricsReadRepository;

  public LyricsService(IReadRepository<Lyrics> lyricsReadRepository)
  {
    _lyricsReadRepository = lyricsReadRepository;
  }

  public async Task<Result<IEnumerable<LyricsViewModel>>> GetLyricsByTrackId(short trackId)
  {
    var spec = new GetLyricsByTrackIdSpec(trackId);
    var result = await _lyricsReadRepository.ListAsync(spec);

    return result;
  }
}

