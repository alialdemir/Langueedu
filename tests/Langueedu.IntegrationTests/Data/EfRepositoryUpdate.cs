using Langueedu.Core.Entities.PlaylistAggregate;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Langueedu.IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
  [Fact]
  public async Task UpdatesItemAfterAddingIt()
  {
    // add a Playlist
    var repository = GetRepository();
    var initialName = Guid.NewGuid().ToString();
    var Playlist = new Playlist(initialName);

    await repository.AddAsync(Playlist);

    // detach the item so we get a different instance
    _dbContext.Entry(Playlist).State = EntityState.Detached;

    // fetch the item and update its title
    var newPlaylist = (await repository.ListAsync())
        .FirstOrDefault(playlist => playlist.PlaylistName == initialName);
    if (newPlaylist == null)
    {
      Assert.NotNull(newPlaylist);
      return;
    }
    Assert.NotSame(Playlist, newPlaylist);
    var newName = Guid.NewGuid().ToString();
    newPlaylist.UpdateName(newName);

    // Update the item
    await repository.UpdateAsync(newPlaylist);
    var test = (await repository.ListAsync());
    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
        .FirstOrDefault(playlist => playlist.PlaylistName == newName);

    Assert.NotNull(updatedItem);
    Assert.NotEqual(Playlist.PlaylistName, updatedItem?.PlaylistName);
    Assert.Equal(newPlaylist.Id, updatedItem?.Id);
  }
}

