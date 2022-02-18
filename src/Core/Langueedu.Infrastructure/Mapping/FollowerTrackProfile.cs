using AutoMapper;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Features.Commands.Playlist.AddFollowerTrack;
using Langueedu.Core.Features.Commands.Playlist.RemoveFollowerTrack;

namespace Langueedu.Infrastructure.Mapping;

public class FollowerTrackProfile : Profile
{
  public FollowerTrackProfile()
  {
    CreateMap<AddFollowerTrackCommand, FollowerTrack>();

    CreateMap<RemoveFollowerTrackCommand, FollowerTrack>();

  }
}

