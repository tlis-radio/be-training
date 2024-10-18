using MediatR;

namespace Core.Domain.Model;

public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>, IAggregateRoot<TIdentity>
    where TIdentity : AbstractIdentity
{
    private readonly Queue<IEvent<TIdentity>> _events = [];
    
    public async Task PublishEvents(IPublisher publisher)
    {
        while(_events.TryDequeue(out IEvent<TIdentity>? @event))
            await publisher.Publish(@event);
    }

    public virtual void Evolve(IEnumerable<IEvent<TIdentity>> events)
    {
        foreach (IEvent<TIdentity> @event in events)
            Evolve(@event);
    }

    protected abstract void Evolve(IEvent<TIdentity> @event);

    protected void Enqueue(IEvent<TIdentity> @event)
    {
        BusynessRuleException.ThrowIfNull(@event, "Domain event should not be equal to null");

        @event.AggregateRootId = Identity;
        @event.OccuredOn = DateTime.UtcNow;
        
        _events.Enqueue(@event);
    }
}