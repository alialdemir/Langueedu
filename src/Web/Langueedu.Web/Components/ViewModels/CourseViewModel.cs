using System.Windows.Input;
using Langueedu.Components.Interfaces;
using Langueedu.Components.Models;
using Langueedu.Components.Services;
using Langueedu.Sdk.Course;
using Langueedu.Sdk.Course.Request;
using Langueedu.Sdk.Course.Response;
using Langueedu.Sdk.Enums;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using static Langueedu.Components.Services.YoutubePlayer;

namespace Langueedu.Components.ViewModels;

public class CourseViewModel : ViewModelBase
{
  private readonly IYoutubePlayer _youtubePlayer;
  private readonly ICourseService _courseService;
  private ICommand _stylishCommand;

  public CourseViewModel(IYoutubePlayer youtubePlayer,
                         ICourseService courseService)
  {
    _youtubePlayer = youtubePlayer;
    _courseService = courseService;
  }

  [Parameter]
  public CreateCourseModel CreateCourse { get; set; }

  public CourseDetailViewModel CourseDetail { get; set; } = new();

  private bool IsTimeActive { get; set; } = true;

  private double _currentDuration;
  public double CurrentDuration
  {
    get => _currentDuration;
    set => SetField(ref _currentDuration, value);
  }

  public List<CourseParticipantDetail> CourseParticipantDetails { get; set; } = new();

  private CourseIndicatorModel _courseIndicator = new();
  public CourseIndicatorModel CourseIndicator
  {
    get => _courseIndicator;
    set => SetField(ref _courseIndicator, value);
  }

  private LyricsViewModel _nextLyrics = new();
  public LyricsViewModel NextLyrics
  {
    get => _nextLyrics;
    set => SetField(ref _nextLyrics, value);
  }

  private LyricsViewModel _currentLyrics = new();
  public LyricsViewModel CurrentLyrics
  {
    get => _currentLyrics;
    set => SetField(ref _currentLyrics, value);
  }

  public short MaxLyricsCount
  {
    get
    {
      if (CourseDetail == null || CourseDetail.Lyrics == null)
        return 0;

      return (short)Math.Ceiling(CourseDetail.Lyrics.Count * (decimal)CreateCourse.CourseLevel / 100);
    }
  }

  public override async Task OnInitializedAsync()
  {
    CourseDetail = await _courseService.CreateCourse(CreateCourse);
    if (CourseDetail == null)
    {
      await HideModal();

      // TODO: alert message

      return;
    }

    IsTimeActive = true;

    CourseDetail
         .Lyrics
         .DistinctBy(x => x.Answer)
         .OrderBy(x => Guid.NewGuid())
         .Take(MaxLyricsCount)
         .ToList()
         .ForEach(item => item.IsShowAnswer = false);

    CourseIndicator.Gaps = MaxLyricsCount;

    await _youtubePlayer.InitYoutube(CourseDetail.YoutubeVideoId);

    YoutubePlayer.OnPlayerReady = new Command(OnPlayerReady);
    YoutubePlayer.OnPlayerStateChange = new Command<YoutubePlayerStateModel>(OnPlayerStateChange);

    SetNextLyrics(0);
  }

  private async Task OnPlayerReady()
  {
    await _youtubePlayer.PlayVideo();

    Services.Timer.Start(TimeSpan.FromSeconds(1), SetCurrentDuration);
  }

  private void OnPlayerStateChange(YoutubePlayerStateModel state)
  {
    // Course finis
    if (state.PlayerState == PlayerState.VideoFinish)
    {
      // TODO: call course finish apis

      HideModal();
    }
  }

  private void SetNextLyrics(int lyricsId)
  {
    int currentLyricsIndex = CourseDetail.Lyrics.FindIndex(x => x.Id == lyricsId);

    // Course the end
    if ((currentLyricsIndex + 1) >= CourseDetail.Lyrics.Count)
      return;

    if (currentLyricsIndex == -1)
      currentLyricsIndex = 0;

    CurrentLyrics = CourseDetail.Lyrics[currentLyricsIndex];

    // Next stylish
    if (CurrentLyrics.IsShowAnswer == false)
    {
      GenerateRandomStylish();
    }

    NextLyrics = CourseDetail.Lyrics[currentLyricsIndex + 1];
  }

  protected override Task HideModal()
  {
    IsTimeActive = false;

    return base.HideModal();
  }

  private async Task<bool> SetCurrentDuration()
  {
    if (!IsTimeActive)
      return false;

    // Duration convert to milliseconds
    CurrentDuration = (await _youtubePlayer.CurrentTime()) * 1000;

    if (CurrentDuration < NextLyrics.Duration)
      return IsTimeActive;

    // If the user does not respond, pause the video
    if ((CurrentDuration + 10000) >= NextLyrics.Duration &&
      CurrentLyrics.IsShowAnswer == false)
    {
      await _youtubePlayer.PauseVideo();

      CourseIndicator.Time = 10;

      Services.Timer.Start(TimeSpan.FromSeconds(1), WaitinAnswerTimer);

      return IsTimeActive;
    }

    SetNextLyrics(NextLyrics.Id);

    System.Console.WriteLine("test");

    await _youtubePlayer.ScrollIntoView($"id{NextLyrics.Id}");

    return IsTimeActive;
  }

  private async Task<bool> WaitinAnswerTimer()
  {
    CourseIndicator.Time--;
    System.Console.WriteLine($"time: {CourseIndicator.Time}");

    if (CourseIndicator.Time <= 0)
    {
      StylishCommandExecute(new StylishModel
      {
        Answer = string.Empty,
      });

      await _youtubePlayer.PlayVideo();
    }

    StateHasChanged();

    return CourseIndicator.Time > 0;
  }

  private async void StylishCommandExecute(StylishModel stylish)
  {
    if (IsBusy || CourseParticipantDetails.Any(x => x.LyricsId == CurrentLyrics.Id))
      return;

    IsBusy = true;

    // If it doesn't respond to the stylish, it's answer empty
    if (!string.IsNullOrEmpty(stylish.Answer))
      CurrentLyrics.IsAnswerCorrect = stylish.Answer == CurrentLyrics.Answer;

    Color color = CurrentLyrics.IsAnswerCorrect.HasValue && CurrentLyrics.IsAnswerCorrect.Value ?
      Color.Success :
      Color.Danger;

    ChangeStylishColor(color, stylish.Answer);

    CurrentLyrics.IsShowAnswer = true;

    Stylish stylishEnum = (Stylish)CurrentLyrics.Stylish.IndexOf(stylish) + 1;

    if (CurrentLyrics.IsAnswerCorrect.HasValue && CurrentLyrics.IsAnswerCorrect.Value)// Hits scenarios
    {
      CourseIndicator.TotalHits += 1;

      int currentLyricsIndex = CourseDetail.Lyrics.FindIndex(x => x.Id == CurrentLyrics.Id);
      short score = ScoreCalculator(currentLyricsIndex);

      CourseIndicator.TotalScore += score;

      AddCourseParticipantDetail(CourseSuccessStatus.Hits,
                                 stylishEnum,
                                 score);
    }
    else if (CurrentLyrics.IsAnswerCorrect.HasValue && !CurrentLyrics.IsAnswerCorrect.Value)// Fails scenarios
    {
      CourseIndicator.TotalFails += 1;

      AddCourseParticipantDetail(CourseSuccessStatus.Fails,
                                 stylishEnum);
    }
    else// Gaps scenarios
    {
      CourseIndicator.TotalUserGaps += 1;

      AddCourseParticipantDetail(CourseSuccessStatus.Gaps,
                                 stylishEnum);
    }

    StateHasChanged();

    await _youtubePlayer.PlayVideo();

    IsBusy = false;
  }

  private void AddCourseParticipantDetail(CourseSuccessStatus courseSuccessStatus,
                                          Stylish stylish,
                                          short score = 0)
  {
    CourseParticipantDetails.Add(new CourseParticipantDetail
    {
      CourseSuccessStatus = courseSuccessStatus,
      LyricsId = CurrentLyrics.Id,
      AnswerTime = CourseIndicator.Time,
      Score = score,
      Stylish = stylish,
    });
  }

  private void ChangeStylishColor(Color color, string answer)
  {
    CurrentLyrics
    .Stylish
    .ForEach(stylish =>
               {
                 // Set default value
                 stylish.Color = Color.Transparent;
                 stylish.IsLeftArrowVisible = false;
                 stylish.IsRightArrowVisible = false;

                 if (stylish.Answer.ToUpperInvariant() == answer.ToUpperInvariant())
                 {
                   stylish.Color = color;
                 }
               });
  }

  private void GenerateRandomStylish()
  {
    CurrentLyrics.Stylish = CourseDetail
        .Lyrics
        .Where(x => x.Answer.ToUpperInvariant() != CurrentLyrics.Answer.ToUpperInvariant())
        .OrderBy(x => Guid.NewGuid())
        .Select(x => new StylishModel
        {
          Answer = x.Answer,
        })
        .Take(3)
        .ToList();

    CurrentLyrics.Stylish.Add(new StylishModel
    {
      Answer = CurrentLyrics.Answer
    });

    CurrentLyrics.Stylish = CurrentLyrics
      .Stylish
      .OrderBy(x => Guid.NewGuid())
      .ToList();
  }

  private short ScoreCalculator(int lyricsIndex)
  {
    if (lyricsIndex <= 0)
      lyricsIndex = 1;

    short score = (short)Math.Round((double)(lyricsIndex * 2));
    return score;
  }

  public ICommand StylishCommand { get => _stylishCommand ??= new Command<StylishModel>(StylishCommandExecute); }
}