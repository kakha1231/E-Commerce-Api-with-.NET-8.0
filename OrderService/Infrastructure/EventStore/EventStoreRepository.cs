using System.Text.Json;
using EventStore.Client;

namespace OrderService.Infrastructure.EventStore;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly EventStoreClient _client;
    
    public EventStoreRepository()
    {
        var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false");
        _client = new EventStoreClient(settings);
    }
    
    public async Task SaveEventAsync<T>(T @event, string streamName)
    {
        var eventData = new EventData(
            Uuid.NewUuid(),
            typeof(T).Name,
            JsonSerializer.SerializeToUtf8Bytes(@event)
        );
        
        await _client.AppendToStreamAsync(streamName, StreamState.Any, new[] { eventData });
    }

    public async Task<IEnumerable<object>> GetEventsAsync(string streamName)
    {
        var result = _client.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start);
        var events = new List<object>();

        await foreach (var resolvedEvent in result)
        {
            var eventType = Type.GetType(resolvedEvent.Event.EventType);
            var @event = JsonSerializer.Deserialize(resolvedEvent.Event.Data.Span, eventType);
            events.Add(@event);
        }

        return events;
    }
}