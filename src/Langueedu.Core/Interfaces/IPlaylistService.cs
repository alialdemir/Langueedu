using System;
using Ardalis.Result;
using Langueedu.Core.Features.Queries.Playlist;
using Langueedu.SharedKernel.ViewModels;

namespace Langueedu.Core.Interfaces
{
    public interface IPlaylistService
    {
        Task<Result<IEnumerable<PlaylistViewModel>>> GetAllPlaylistAsync();
    }
}

