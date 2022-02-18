using Ardalis.Result;
using Langueedu.Core.Features.Queries.Playlist.GetAllPlaylist;
using Langueedu.SharedKernel.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Langueedu.API.Controllers;

[ApiVersion("1.0")]
public class PlaylistsController : BaseApiController
{
  private readonly IMediator _mediator;

  public PlaylistsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [SwaggerOperation(
      Summary = "Gets a list of all PlayList",
      Description = "Gets a list of all PlayList",
      OperationId = "PlayList.GetAllPlaylist",
      Tags = new[] { "PlayList Endpoints" })
  ]
  [HttpGet]
  [ProducesResponseType(typeof(Result<IEnumerable<PlaylistViewModel>>), StatusCodes.Status200OK)]
  public async Task<IActionResult> GetAllPlaylists()
  {
    var playList = await _mediator.Send(new GetAllPlaylistsQuery());

    return playList.ToActionResult();
  }
}

