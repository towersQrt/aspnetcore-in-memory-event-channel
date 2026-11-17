using Kendal.Web.Abstractions.Events;

namespace Kendal.Web.Events;

internal sealed record EventEnvelope(string EventType, IEvent Evento);
