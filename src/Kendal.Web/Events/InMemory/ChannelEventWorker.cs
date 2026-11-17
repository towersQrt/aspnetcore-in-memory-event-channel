namespace Kendal.Web.Events.InMemory;

internal sealed class ChannelEventWorker : BackgroundService
{
    private readonly ChannelEventPublisher _publisher;
    private readonly EventDispatcher _dispatcher;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ChannelEventWorker> _logger;

    public ChannelEventWorker(
        ChannelEventPublisher publisher,
        EventDispatcher dispatcher,
        IServiceScopeFactory scopeFactory,
        ILogger<ChannelEventWorker> logger)
    {
        _publisher = publisher;
        _dispatcher = dispatcher;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (EventEnvelope envelope in _publisher.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await using AsyncServiceScope scope = _scopeFactory.CreateAsyncScope();
                await _dispatcher.DispatchAsync(envelope.Evento, scope.ServiceProvider, stoppingToken);
            }
            catch (Exception exception) when (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogError(exception, "Error al procesar el evento {EventType}", envelope.EventType);
            }
        }
    }
}