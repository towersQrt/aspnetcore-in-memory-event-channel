using Kendal.Web.Abstractions.Events;
using Kendal.Web.Events;
using Kendal.Web.Events.InMemory;

namespace Kendal.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.Scan(x => x
            .FromAssemblyOf<IEvent>()
            .AddClasses(y => y.AssignableTo(typeof(IEventHandler<>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddSingleton<EventDispatcher>();
        services.AddSingleton<ChannelEventPublisher>();
        services.AddSingleton<IEventPublisher>(x => x.GetRequiredService<ChannelEventPublisher>());
        services.AddHostedService<ChannelEventWorker>();

        return services;
    }
}
