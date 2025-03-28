namespace OrderService.Infrastructure.EventStore;

public interface IEventStoreRepository
{
    Task SaveEventAsync<T>(T @event, string streamName);
    Task<IEnumerable<object>> GetEventsAsync(string streamName);
}