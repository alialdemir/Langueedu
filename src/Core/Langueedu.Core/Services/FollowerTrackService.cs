using Ardalis.Result;
using Langueedu.Core.Entities.PlaylistAggregate;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Specifications.Track;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Services;

public class FollowerTrackService : IFollowerTrackService
{
  private readonly IRepository<FollowerTrack> _followerTrack;
  private readonly IRepository<Track> _trackRepository;
  private readonly IReadRepository<FollowerTrack> _followedTrackReadRepository;

  public FollowerTrackService(
    IRepository<FollowerTrack> _followerTrack,
    IRepository<Track> trackRepository,
    IReadRepository<FollowerTrack> followedTrackReadRepository)
  {
    this._followerTrack = _followerTrack;
    _trackRepository = trackRepository;
    _followedTrackReadRepository = followedTrackReadRepository;
  }

  public Task<Result<bool>> AddAsync(FollowerTrack followerTrack)
  {
    return ToggleFollow(followerTrack,
    followedStatus: true,
    successMessage: "Track was followed!",
    callback: (track) => track.AddFollowerTrack(new FollowerTrack(followerTrack.TrackId, followerTrack.UserId)));
  }

  public Task<Result<bool>> DeleteAsync(FollowerTrack followerTrack)
  {
    return ToggleFollow(followerTrack,
    followedStatus: false,
    successMessage: "Track was unfollowed!",
    callback: (track) => track.RemoveFollowerTrack(new FollowerTrack(followerTrack.TrackId, followerTrack.UserId)));
  }

  private async Task<Result<bool>> ToggleFollow(FollowerTrack followerTrack,
                                                bool followedStatus,
                                                string successMessage,
                                                Action<Track> callback)
  {
    Track? track = await _trackRepository.GetByIdAsync(followerTrack.TrackId);
    if (followerTrack == null || track == null)
      return Result<bool>.Error("Followed information cannot be null.");

    Result<bool> isTrackFollowed = await IsFollowed(followerTrack.UserId, followerTrack.TrackId);
    if (isTrackFollowed.Value == followedStatus)
      return Result<bool>.Error(isTrackFollowed.SuccessMessage);

    callback(track);

    await _followerTrack.SaveChangesAsync();

    return Result<bool>.Success(true, successMessage);
  }

  private async Task<Result<bool>> IsFollowed(string userId, short trackId)
  {
    var spec = new GetFollowerTrackByUserIdSpec(userId, trackId);
    var isFollowed = await _followedTrackReadRepository
      .AnyAsync(spec);

    string successMessage = isFollowed ? "The track is already being followed!" : "The track is not follow!";

    return Result<bool>.Success(isFollowed, successMessage);
  }
}

