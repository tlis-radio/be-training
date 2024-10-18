using MediatR;

namespace Core.Domain.Model;

public interface IEvent : INotification; 

public interface IEvent<TIdentity> : IEvent where TIdentity : AbstractIdentity 
{
    public TIdentity AggregateRootId { get; internal set; }
    
    public DateTime OccuredOn { get; internal set; }

}