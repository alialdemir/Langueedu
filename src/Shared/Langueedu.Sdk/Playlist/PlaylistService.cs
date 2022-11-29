using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Langueedu.Sdk.Interfaces;
using Langueedu.Sdk.Playlist.Response;
using Langueedu.Sdk.Utilities;

namespace Langueedu.Sdk.Playlist
{
  public class PlaylistService : ServiceBase, IPlaylistService
  {
    private const string API_URL_BASE = "/v1/Playlists";

    public PlaylistService(IHttpClientFactory clientFactory,
                           IToastService toastService)
        : base(clientFactory.CreateClient("LangueeduApi"), toastService)
    {
    }

    public async Task<Result<IEnumerable<PlaylistViewModel>>> GetAllPlaylistsAsync()
    {
      string uri = UriHelper.CombineUri(Configs.GatewaEndpoint, API_URL_BASE);

      var response = await GetAsync<IEnumerable<PlaylistViewModel>>(uri);
      return response;
    }
  }
}
