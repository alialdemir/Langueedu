﻿using System.Windows.Input;
using Langueedu.Components.Interfaces;
using Langueedu.Components.Models;
using Langueedu.Web.Shared.Utilities;
using Microsoft.JSInterop;

namespace Langueedu.Components.Services;

public class YoutubePlayer : IYoutubePlayer
{
  public enum PlayerState
  {
    VideoFinish = 0,
    Unknown = 100,
  }

  private readonly IJSRuntime _jSRuntime;

  public YoutubePlayer(IJSRuntime jSRuntime)
  {
    _jSRuntime = jSRuntime;
  }

  public static ICommand OnPlayerReady;
  public static Command<YoutubePlayerStateModel> OnPlayerStateChange;

  public async Task InitYoutube(string videoId)
  {
    await _jSRuntime.InvokeVoidAsync("youtubePlayer.init", videoId);

  }

  public async Task<double> CurrentTime()
  {
    return await _jSRuntime.InvokeAsync<double>("youtubePlayer.currentTime");
  }

  public async Task ScrollIntoView(string elementId)
  {
    await _jSRuntime.InvokeVoidAsync("youtubePlayer.scrollIntoView", elementId);
  }

  public async Task PlayVideo()
  {
    await _jSRuntime.InvokeVoidAsync("youtubePlayer.playVideo");
  }

  public async Task PauseVideo()
  {
    await _jSRuntime.InvokeVoidAsync("youtubePlayer.pauseVideo");
  }

  public async Task StopVideo()
  {
    await _jSRuntime.InvokeVoidAsync("youtubePlayer.stopVideo");
  }

  public async Task LoadVideoById(string videoId, short startSeconds = 0)
  {
    await _jSRuntime.InvokeVoidAsync("youtubePlayer.loadVideoById", videoId, startSeconds);
  }

  [JSInvokable]
  public static void PlayerReady(object data)
  {
    OnPlayerReady?.Execute(null);
  }

  [JSInvokable]
  public static void PlayerStateChange(YoutubePlayerStateModel youtubePlayerState)
  {
    OnPlayerStateChange?.Execute(youtubePlayerState);
  }
}
