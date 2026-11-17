using System.Threading.Channels;
using Kendal.Web.Abstractions.Events;

namespace Kendal.Web.Events.InMemory;

internal sealed class ChannelEventPublisher : IEventPublisher
{
    private readonly Channel<EventEnvelope> _channel = Channel.CreateUnbounded<EventEnvelope>(new UnboundedChannelOptions
    {
        SingleReader = true,
        SingleWriter = false
    });

    public ChannelReader<EventEnvelope> Reader => _channel.Reader;

    public Task PublishAsync<TEvent>(TEvent evento, CancellationToken ct) where TEvent : IEvent
    {
        return _channel.Writer.WriteAsync(new EventEnvelope(GetDisplayName(evento.GetType()), evento), ct).AsTask();
    }

    private static string GetDisplayName(Type type)
    {
        return type.DeclaringType is null ? type.Name : $"{type.DeclaringType.Name}.{type.Name}";
    }
}
