namespace Kendal.Web.Abstractions.Events;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    Task Handle(TEvent evento, CancellationToken ct);
}
