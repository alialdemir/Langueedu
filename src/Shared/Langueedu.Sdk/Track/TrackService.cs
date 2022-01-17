using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Langueedu.Sdk.Models;
using Langueedu.Sdk.Playlist.Response;
using Langueedu.Sdk.Utilities;

namespace Langueedu.Sdk.Track
{
    public class TrackService : ServiceBase, ITrackService
    {
        private const string API_URL_BASE = "/v1/Tracks";

        public TrackService(IHttpClientFactory clientFactory)
            : base(clientFactory.CreateClient("LangueeduApi"))
        {
        }

        public async Task<Result<TrackDetailViewModel>> GetTrackDetail(int trackId)
        {
            if (trackId <= 0)
            {
                return Result<TrackDetailViewModel>.Invalid(new List<ValidationError>
                {
                     new  ValidationError
                     {
                         ErrorMessage = "Track id cannot be zero or less than zero.",
                         Identifier=nameof(trackId),
                         Severity= ValidationSeverity.Error
                    }
                });
            }
            string uri = UriHelper.CombineUri(Configs.GatewaEndpoint, API_URL_BASE, trackId.ToString());

            var response = await GetAsync<TrackDetailViewModel>(uri);
            return response;
        }
    }
}
