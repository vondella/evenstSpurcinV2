namespace user.cmd.api.Shared.Domains;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @event);
    Task<List<EventModel>> FindByAggregate(Guid aggregateId);
    Task<List<EventModel>> FindAllAsync();
}