using Ardalis.Result;
using Langueedu.Core.Features.Commands.Playlist.FollowerTrack;
using Langueedu.Core.Features.Queries.Track.GetTrackDetailById;
using Langueedu.SharedKernel.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Langueedu.API.Controllers;

[ApiVersion("1.0")]
public class TracksController : BaseApiController
{
  private readonly IMediator _mediator;

  public TracksController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [SwaggerOperation(
      Summary = "Get a detail of track",
      Description = "Get a detail of track",
      OperationId = "Track.GetTrackDetail",
      Tags = new[] { "Tracks Endpoints" })
  ]
  [HttpGet("{trackId}")]
  [ProducesResponseType(typeof(Result<TrackDetailViewModel>), StatusCodes.Status200OK)]
  public async Task<IActionResult> GetTrackDetail(short trackId)
  {
    var result = await _mediator.Send(new GetTrackDetailByIdQuery(trackId));

    return result.ToActionResult();
  }

  [SwaggerOperation(
      Summary = "Follow Track",
      Description = "Follows a track",
      OperationId = "Track.FollowTrack",
      Tags = new[] { "Tracks Endpoints" })
  ]
  [HttpPost("{trackId}/Follow")]
  [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
  public async Task<IActionResult> FollowTrack([FromRoute] short trackId)
  {
    var result = await _mediator.Send(new AddFollowerTrackCommand(UserId, trackId));

    return result.ToActionResult();
  }
}

