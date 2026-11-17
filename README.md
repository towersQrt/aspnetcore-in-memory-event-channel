# aspnetcore-in-memory-event-channel

Ejemplo mínimo del patrón Productor-Consumidor en .NET 10 con `System.Threading.Channels`.

## Cómo funciona

`IEventPublisher` escribe el evento en un `Channel`. Un `BackgroundService` (`ChannelEventWorker`) lo lee y ejecuta los `IEventHandler<T>` registrados, cada evento en su propio scope. Los handlers se registran solos con Scrutor:

```csharp
builder.Services.AddEvents();
```

El evento y el handler de ejemplo están en `src/Kendal.Web/Samples/Ping`.

## Ejecutar

```bash
dotnet run --project src/Kendal.Web
```

Abre la página: cada visita publica un evento y el log del handler aparece en la consola, emitido por el worker.
