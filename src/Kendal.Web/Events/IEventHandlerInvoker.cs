using Kendal.Web.Abstractions.Events;

namespace Kendal.Web.Events;

internal interface IEventHandlerInvoker
{
    Task InvokeAsync(IEvent evento, IServiceProvider serviceProvider, CancellationToken ct);
}
