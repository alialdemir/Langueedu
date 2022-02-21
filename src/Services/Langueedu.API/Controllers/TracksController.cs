using Ardalis.Result;
using Langueedu.Core.Features.Commands.Playlist.AddFollowerTrack;
using Langueedu.Core.Features.Commands.Playlist.RemoveFollowerTrack;
using Langueedu.Core.Features.Commands.Track.TrackAdapter;
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
    var result = await _mediator.Send(new GetTrackDetailByIdQuery(trackId, UserId));

    return result.ToActionResult();
  }

  [SwaggerOperation(
      Summary = "Follow Track",
      Description = "Follow a track",
      OperationId = "Track.FollowTrack",
      Tags = new[] { "Tracks Endpoints" })
  ]
  [HttpPost("{trackId}/Follow")]
  [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
  public async Task<IActionResult> FollowTrack([FromRoute] short trackId)
  {
    var result = await _mediator.Send(new AddFollowerTrackCommand(UserId, trackId));

    return result.ToActionResult();
  }

  [SwaggerOperation(
      Summary = "UnFollow Track",
      Description = "UnFollow a track",
      OperationId = "Track.UnFollow",
      Tags = new[] { "Tracks Endpoints" })
  ]
  [HttpDelete("{trackId}/Follow")]
  [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
  public async Task<IActionResult> UnFollow([FromRoute] short trackId)
  {
    var result = await _mediator.Send(new RemoveFollowerTrackCommand(UserId, trackId));

    return result.ToActionResult();
  }

  [SwaggerOperation(
      Summary = "Upload song",
      Description = "Upload song",
      OperationId = "Track.UploadSong",
      Tags = new[] { "Tracks Endpoints" })
  ]
  [HttpPost]
  public async Task<IActionResult> UploadSong([FromForm] IFormFile file)
  {
    if (!file.FileName.EndsWith(".json"))
      return BadRequest();

    await _mediator.Publish(new TrackAdapterCommand(file));

    return Ok();
  }
}