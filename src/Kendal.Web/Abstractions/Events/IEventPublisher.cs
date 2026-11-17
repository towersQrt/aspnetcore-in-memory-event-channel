namespace Kendal.Web.Abstractions.Events;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent evento, CancellationToken ct) where TEvent : IEvent;
}
