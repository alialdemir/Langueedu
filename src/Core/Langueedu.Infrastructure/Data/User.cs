using Ardalis.GuardClauses;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Langueedu.Infrastructure.Data;

public partial class User : IdentityUser, IUser
{
  public string LanguageCode { get; set; }
  public bool IsActive { get; set; }

  private readonly List<FollowerTrack> _followerTracks = new();

  public IReadOnlyCollection<FollowerTrack> FollowerTracks => _followerTracks.AsReadOnly();
}