using System.Collections.Generic;
using System.Threading.Tasks;
using Langueedu.Sdk.Playlist.Response;

namespace Langueedu.Sdk.Playlist
{
  public interface IPlaylistService
  {
    Task<Result<IEnumerable<PlaylistViewModel>>> GetAllPlaylistsAsync();
  }
}
