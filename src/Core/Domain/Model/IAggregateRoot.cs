using MediatR;

namespace Core.Domain.Model;

public interface IAggregateRoot<TIdentity> : IEntity<TIdentity> where TIdentity : AbstractIdentity
{
    public Task PublishEvents(IPublisher publisher);
    
    public void Evolve(IEnumerable<IEvent<TIdentity>> events);
}