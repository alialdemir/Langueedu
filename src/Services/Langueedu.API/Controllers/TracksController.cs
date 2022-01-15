using System;
using Ardalis.Result;
using Langueedu.Core.Features.Queries.Track.GetTrackDetailById;
using Langueedu.Core.Specifications;
using Langueedu.SharedKernel.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Langueedu.API.Controllers
{
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
            Tags = new[] { "GetTrackDetail Endpoints" })
        ]
        [HttpGet("{trackId}")]
        [ProducesResponseType(typeof(Result<TrackDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTrackDetail(int trackId)
        {
            var playList = await _mediator.Send(new GetTrackDetailByIdQuery(trackId));

            return playList.ToActionResult();
        }
    }
}

