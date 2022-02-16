let player = undefined
const namespace = 'Langueedu.Web.Components'

const onPlayerReady = (event) => {
  DotNet.invokeMethodAsync(namespace, 'PlayerReady', event)
}

const onPlayerStateChange = (event) => {
  DotNet.invokeMethodAsync(namespace, 'PlayerStateChange', event)
}

const insertYoutubeApiScript = () => {
  if (document.getElementById('youtubePlayerScript') !== null) {
    return
  }

  var tag = document.createElement('script')
  tag.src = 'https://www.youtube.com/player_api'
  tag.id = 'youtubePlayerScript'
  var firstScriptTag = document.getElementsByTagName('script')[0]
  firstScriptTag.parentNode.insertBefore(tag, firstScriptTag)
}

const init = (videoId) => {
  insertYoutubeApiScript()

  const intervalValue = setInterval(() => {
    if (typeof YT === 'undefined' || typeof (YT || {}).Player === 'undefined') {
      return
    }

    clearInterval(intervalValue)

    player = new YT.Player('LeYoutubePlayer', {
      height: '315',
      width: '100%',
      playerVars: {
        playsinline: 1,
        accelerometer: 1,
        gyroscope: 1,
        rel: 0,
        showinfo: 0,
        // controls: 0,
        'clipboard-write': 1,
        'encrypted-media': 1,
        'picture-in-picture': 1,
      },
      videoId: videoId,
      events: {
        onReady: (events) => onPlayerReady(events),
        onStateChange: (events) => onPlayerStateChange(events),
      },
    })
  }, 100)
}

const currentTime = () => {
  if (!player) {
    return 0
  }

  return player.playerInfo.currentTime
}

const playVideo = () => {
  if (player) {
    player.playVideo()
  }
}

const pauseVideo = () => {
  if (player) {
    player.pauseVideo()
  }
}

const stopVideo = () => {
  if (player) {
    player.stopVideo()
  }
}

const loadVideoById = (videoId, startSeconds = 0) => {
  if (player && videoId) {
    player.loadVideoById(videoId, startSeconds)
  }
}

const scrollIntoView = (elementId) => {
  if (elementId) {
    document.getElementById(elementId).scrollIntoView({
      behavior: 'auto',
      block: 'center',
      inline: 'center',
    })
  }
}

window.youtubePlayer = {
  init,
  currentTime,
  playVideo,
  pauseVideo,
  stopVideo,
  loadVideoById,
  scrollIntoView,
}
