using Langueedu.Sdk.Playlist.Response;
using Langueedu.Sdk.Track;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components.ViewModels
{
    public class TrackCoverViewModel : ViewModelBase
    {
        private readonly ITrackService _trackService;

        public TrackCoverViewModel(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [Parameter]
        public int TrackId { get; set; }

        private TrackDetailViewModel _trackDetail = new();
        public TrackDetailViewModel TrackDetail
        {
            get => _trackDetail;
            set => Set(ref _trackDetail, value);
        }


        public override async Task OnInitializedAsync()
        {
            TrackDetail = await _trackService.GetTrackDetail(TrackId);
        }
    }
}
