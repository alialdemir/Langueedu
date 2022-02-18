
namespace Langueedu.Web.Components.Interfaces;
public interface IYoutubePlayer
{
  Task<double> CurrentTime();
  Task InitYoutube(string videoId);
  Task LoadVideoById(string videoId, short startSeconds = 0);
  Task PauseVideo();
  Task PlayVideo();
  Task ScrollIntoView(string elementId);
  Task StopVideo();
}
