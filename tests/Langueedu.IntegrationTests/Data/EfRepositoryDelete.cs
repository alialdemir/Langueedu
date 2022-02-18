using Langueedu.Core.Entities.PlaylistAggregate;
using Xunit;

namespace Langueedu.IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
  [Fact]
  public async Task DeletesItemAfterAddingIt()
  {
    // add a Playlist
    var repository = GetRepository();
    var initialName = Guid.NewGuid().ToString();
    var Playlist = new Playlist(initialName);
    await repository.AddAsync(Playlist);

    // delete the item
    await repository.DeleteAsync(Playlist);

    // verify it's no longer there
    Assert.DoesNotContain(await repository.ListAsync(),
        Playlist => Playlist.PlaylistName == initialName);
  }
}

