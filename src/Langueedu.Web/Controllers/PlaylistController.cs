using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using Langueedu.Core.Features.Queries.Playlist;
using Langueedu.SharedKernel.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Langueedu.Web.Controllers;

public class PlaylistController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PlaylistController(IMediator mediator,
                              IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [SwaggerOperation(
        Summary = "Gets a list of all PlayList",
        Description = "Gets a list of all PlayList",
        OperationId = "PlayList.GetAllPlaylist",
        Tags = new[] { "PlayList Endpoints" })
    ]
    [HttpGet]
    public async Task<IActionResult> GetAllPlaylist()
    {
        var playList = await _mediator.Send(new GetAllPlaylistQuery());

        return playList.ToActionResult();
    }
}

