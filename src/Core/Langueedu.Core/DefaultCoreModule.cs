using Autofac;
using Langueedu.Core.Adapters;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Services;

namespace Langueedu.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder
       .RegisterType<PlaylistService>()
       .As<IPlaylistService>()
       .InstancePerLifetimeScope();

    builder
       .RegisterType<TrackService>()
       .As<ITrackService>()
       .InstancePerLifetimeScope();

    builder
       .RegisterType<FollowerTrackService>()
       .As<IFollowerTrackService>()
       .InstancePerLifetimeScope();

    builder
       .RegisterType<CourseService>()
       .As<ICourseService>()
       .InstancePerLifetimeScope();

    builder
       .RegisterType<LyricsService>()
       .As<ILyricsService>()
       .InstancePerLifetimeScope();

    builder
       .RegisterType<TrackAdapter>()
       .As<ITrackAdapter>()
       .InstancePerLifetimeScope();
  }
}
