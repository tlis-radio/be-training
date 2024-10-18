namespace Core.Domain.Model;

public interface IEventStore
{
    public Task<List<IEvent<TIdentity>>> RetrieveEvents<TIdentity>(TIdentity aggregateRootId,
        CancellationToken cancellationToken = default)
        where TIdentity : AbstractIdentity;

    public Task AppendEvent<TIdentity>(IEvent<TIdentity> @event, CancellationToken cancellationToken = default)
        where TIdentity : AbstractIdentity;
}