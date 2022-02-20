using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels.Course;

namespace Langueedu.Core.Interfaces;

public interface ILyricsService
{
  Task<Result<IEnumerable<LyricsViewModel>>> GetLyricsByTrackId(short trackId);
}
