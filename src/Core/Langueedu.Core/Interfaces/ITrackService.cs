using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels;

namespace Langueedu.Core.Interfaces
{
    public interface ITrackService
    {
        Task<Result<TrackDetailViewModel>> GetTrackDetailByIdAsync(int trackId);
    }
}

