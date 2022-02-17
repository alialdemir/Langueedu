using Ardalis.Result;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Specifications.Track;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Services;

public class FollowerTrackService : IFollowerTrackService
{
  private readonly IRepository<FollowerTrack> _followerTrack;
  private readonly IReadRepository<FollowerTrack> _followedTrackReadRepository;

  public FollowerTrackService(
    IRepository<FollowerTrack> _followerTrack,
    IReadRepository<FollowerTrack> followedTrackReadRepository)
  {
    this._followerTrack = _followerTrack;
    _followedTrackReadRepository = followedTrackReadRepository;
  }

  public async Task<Result<string>> Add(FollowerTrack followerTrack)
  {
    if (followerTrack == null)
      return Result<string>.Error("Followed information cannot be null.");

    Result<bool> isTrackFollowed = await IsFollowed(followerTrack.UserId, followerTrack.TrackId);
    if (isTrackFollowed.Value)
      return Result<string>.Error(isTrackFollowed.SuccessMessage);

    await _followerTrack.AddAsync(followerTrack);

    await _followerTrack.SaveChangesAsync();

    return Result<string>.Success("Track was followed!");
  }

  public async Task<Result<string>> DeleteAsync(string userId, short trackId)
  {
    Result<bool> isTrackFollowed = await IsFollowed(userId, trackId);
    if (!isTrackFollowed.Value)
      return Result<string>.Error(isTrackFollowed.SuccessMessage);

    var spec = new GetFollowerTrackByUserIdSpec(userId, trackId);
    FollowerTrack? followerTrack = await _followerTrack.GetBySpecAsync(spec);

    await _followerTrack.DeleteAsync(followerTrack);

    await _followerTrack.SaveChangesAsync();

    return Result<string>.Success("Track was unfollowed!");
  }

  public async Task<Result<bool>> IsFollowed(string userId, short trackId)
  {
    var spec = new GetFollowerTrackByUserIdSpec(userId, trackId);
    var isFollowed = await _followedTrackReadRepository
      .AnyAsync(spec);

    string successMessage = isFollowed ? "The track is already being followed!" : "The track is not follow!";

    return Result<bool>.Success(isFollowed, successMessage);
  }
}

