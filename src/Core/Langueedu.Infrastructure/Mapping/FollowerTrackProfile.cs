using AutoMapper;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Features.Commands.Playlist.FollowerTrack;

namespace Langueedu.Infrastructure.Mapping;

public class FollowerTrackProfile : Profile
{
  public FollowerTrackProfile()
  {
    CreateMap<FollowerTrack, AddFollowerTrackCommand>()
     .ReverseMap();
  }
}

