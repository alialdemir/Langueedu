using System;
using Ardalis.Result;
using Langueedu.SharedKernel.ViewModels;
using MediatR;

namespace Langueedu.Core.Features.Queries.Track.GetTrackDetailById
{
    public class GetTrackDetailByIdQuery : IRequest<Result<TrackDetailViewModel>>
    {
        public GetTrackDetailByIdQuery(int trackId)
        {
            TrackId = trackId;
        }
        public int TrackId { get; set; }
    }
}

