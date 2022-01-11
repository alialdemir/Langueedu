using Autofac;
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
    }
}

