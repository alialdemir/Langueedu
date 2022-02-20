using Langueedu.Core.Services;
using Langueedu.Core.Specifications.Lyrics;
using Langueedu.SharedKernel.Interfaces;
using Langueedu.SharedKernel.ViewModels.Course;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Services;

public class GetLyricsByTrackId
{
  private Mock<IReadRepository<Langueedu.Core.Entities.PlaylistAggregate.Lyrics>> _mockRepo = new Mock<IReadRepository<Langueedu.Core.Entities.PlaylistAggregate.Lyrics>>();
  private LyricsService _lyricsService;
  private readonly List<LyricsViewModel> _lyricsViewModels = new List<LyricsViewModel>()
  {
    new LyricsViewModel(){ LyricsId=1,}
  };

  public GetLyricsByTrackId()
  {
    _mockRepo
      .Setup(x => x.ListAsync(It.IsAny<GetLyricsByTrackIdSpec>(), default).Result)
      .Returns(_lyricsViewModels);

    _lyricsService = new LyricsService(_mockRepo.Object);
  }

  [Fact]
  public async Task ReturnsListsGivenLyrics()
  {
    var result = await _lyricsService.GetLyricsByTrackId(trackId: 1);

    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
    Assert.Contains(_lyricsViewModels.FirstOrDefault(), result.Value);
  }
}

