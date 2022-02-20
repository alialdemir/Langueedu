using Langueedu.Core.Entities.CourseAggregate;
using Langueedu.SharedKernel;
using Langueedu.SharedKernel.Interfaces;

namespace Langueedu.Core.Entities.PlaylistAggregate;

public class Lyrics : BaseEntity, IAggregateRoot
{
  public Lyrics(string text, string answer, int time)
  {
    Text = text;
    Answer = answer;
    Time = time;
  }

  public Lyrics()
  {

  }

  public Track Track { get; set; }

  public string Text { get; }
  public string Answer { get; }
  public int Time { get; }

  private readonly List<CourseParticipantDetail> _duelParticipantDetails = new();

  public IReadOnlyCollection<CourseParticipantDetail> DuelParticipantDetails => _duelParticipantDetails.AsReadOnly();
}

