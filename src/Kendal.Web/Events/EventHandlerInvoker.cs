using Kendal.Web.Abstractions.Events;

namespace Kendal.Web.Events;

internal sealed class EventHandlerInvoker<TEvent> : IEventHandlerInvoker where TEvent : IEvent
{
    public async Task InvokeAsync(IEvent evento, IServiceProvider serviceProvider, CancellationToken ct)
    {
        IEnumerable<IEventHandler<TEvent>> lstHandler = serviceProvider.GetServices<IEventHandler<TEvent>>();

        foreach (IEventHandler<TEvent> handler in lstHandler)
        {
            await handler.Handle((TEvent)evento, ct);
        }
    }
}
