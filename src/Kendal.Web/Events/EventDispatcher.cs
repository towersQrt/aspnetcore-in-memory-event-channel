using Kendal.Web.Abstractions.Events;
using System.Collections.Concurrent;

namespace Kendal.Web.Events;

internal sealed class EventDispatcher
{
    private readonly ConcurrentDictionary<Type, IEventHandlerInvoker> _dicEventHandler = new ConcurrentDictionary<Type, IEventHandlerInvoker>();

    public Task DispatchAsync(IEvent evento, IServiceProvider service, CancellationToken ct)
    {
        IEventHandlerInvoker invoker = _dicEventHandler.GetOrAdd(evento.GetType(), CreateInvoker);
        return invoker.InvokeAsync(evento, service, ct);
    }

    private static IEventHandlerInvoker CreateInvoker(Type eventType)
    {
        return (IEventHandlerInvoker)Activator.CreateInstance(typeof(EventHandlerInvoker<>).MakeGenericType(eventType))!;
    }
}