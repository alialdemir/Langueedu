using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels;

namespace Langueedu.Core.Interfaces;

public interface IPlaylistService
{
  Task<Result<IEnumerable<PlaylistViewModel>>> GetAllPlaylistsAsync();
}

