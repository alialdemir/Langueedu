using Langueedu.Core.Entities.PlaylistAggregate;
using Xunit;

namespace Langueedu.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsPlaylistAndSetsId()
  {
    var testPlaylistName = "testPlaylist";
    var repository = GetRepository();
    var Playlist = new PlayList(testPlaylistName);

    await repository.AddAsync(Playlist);

    var newPlaylist = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal(testPlaylistName, newPlaylist?.PlaylistName);
    Assert.True(newPlaylist?.Id > 0);
  }
}

