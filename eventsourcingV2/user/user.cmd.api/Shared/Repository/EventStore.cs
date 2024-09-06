using buildingblock.Exceptions;
using MassTransit;
using user.cmd.api.Shared.Domains;
using user.cmd.api.Shared.Domains.Aggregates;
using user.cmd.core.events;

namespace user.cmd.api.Shared.Repository;

public sealed  class EventStore:IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public EventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }
    public async  Task SaveEventAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggregate(aggregateId);
        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyExecption("concurrency error");

        var version = expectedVersion;
        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel
            {
                Timestamp = DateTime.Now,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(UserAggregates),
                Version = version,
                EventType = eventType,
                EvenData = @event
            };
            await _eventStoreRepository.SaveAsync(eventModel);
            //await PublishEvent.PublishAsync(_publishEndpoint, @event);

        }
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindByAggregate(aggregateId);
        if (eventStream == null || !eventStream.Any())
            throw new NotFoundException("Incorrect AggregateId Provided");

        return eventStream.OrderBy(x => x.Version).Select(x => x.EvenData).ToList();

    }

    public async  Task<List<Guid>> GetAggregateIdAsync()
    {
        var eventStream = await _eventStoreRepository.FindAllAsync();
        if (eventStream == null && !eventStream.Any())
            throw new ArgumentNullException(nameof(eventStream), "colud not retrieve eventstream from event store");
        return eventStream.Select(x => x.AggregateIdentifier).Distinct().ToList();

    }
}