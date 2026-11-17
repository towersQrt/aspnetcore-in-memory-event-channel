using Kendal.Web.Abstractions.Events;

namespace Kendal.Web.Samples.Ping;

public static partial class Ping
{
    public sealed record Event(string Message) : IEvent;

    internal sealed class Handler : IEventHandler<Event>
    {
        private readonly ILogger<Handler> _logger;

        public Handler(ILogger<Handler> logger)
        {
            _logger = logger;
        }

        public Task Handle(Event evento, CancellationToken ct)
        {
            _logger.LogInformation("Mensaje recibido: {Message}", evento.Message);

            return Task.CompletedTask;
        }
    }
}
