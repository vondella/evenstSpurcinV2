using MassTransit;
using user.cmd.api.Shared.Domains;
using user.cmd.api.Shared.Domains.Aggregates;
using user.cmd.api.Shared.Helpers;

namespace user.cmd.api.Shared.Repository;

public sealed  class EventSourcingHandler:IEventSourcingHandle<UserAggregates>
{
    private readonly IEventStore _eventStore;
    private readonly IPublishEndpoint _endPoint;
    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    public Task SaveAsync(AggregateRoot aggregateRoot)
    {
        throw new NotImplementedException();
    }

    public async  Task<UserAggregates> GetById(Guid Id)
    {
        var aggregate = new UserAggregates();
        var events = await _eventStore.GetEventsAsync(Id);
        if (events == null || !events.Any()) return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();
        return aggregate;
    }

    public async Task RepublishEventAsync()
    {
        var aggregateIds = await _eventStore.GetAggregateIdAsync();
        if (aggregateIds == null && !aggregateIds.Any()) return;

        foreach (var aggregateId in aggregateIds)
        {
            var aggregate = await GetById(aggregateId);
            if (aggregate == null && !aggregateIds.Any()) continue;
            var events = await _eventStore.GetEventsAsync(aggregateId);
            foreach (var @event in events)
            {
                await PublishEvent.PublishAsync(_endPoint, @event);
            }
        }
    }
}