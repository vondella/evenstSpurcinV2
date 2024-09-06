namespace user.cmd.api.Shared.Domains;

public interface IEventSourcingHandle<T>
{
    Task SaveAsync(AggregateRoot aggregateRoot);
    Task<T> GetById(Guid Id);
    Task RepublishEventAsync();
}