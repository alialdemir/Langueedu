using System.Text.Json;
using Langueedu.Core.Adapters;
using MediatR;

namespace Langueedu.Core.Features.Commands.Track.TrackAdapter;

public class TrackAdapterCommandHandler : INotificationHandler<TrackAdapterCommand>
{
  private readonly ITrackAdapter _trackAdapter;

  public TrackAdapterCommandHandler(ITrackAdapter trackAdapter)
  {
    _trackAdapter = trackAdapter;
  }

  public async Task Handle(TrackAdapterCommand notification, CancellationToken cancellationToken)
  {
    // TODO: The file extension and file size must be checked!
    StreamReader reader = new StreamReader(notification.File.OpenReadStream());
    string text = reader.ReadToEnd();
    List<Song> songs = JsonSerializer.Deserialize<List<Song>>(text);

    await _trackAdapter.GetTracks("Top Lyrics", songs);
  }
}

