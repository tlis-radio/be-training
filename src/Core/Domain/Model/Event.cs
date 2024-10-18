namespace Core.Domain.Model;

public abstract record Event<TIdentity> : ValueObject, IEvent<TIdentity>
    where TIdentity : AbstractIdentity
{
    TIdentity IEvent<TIdentity>.AggregateRootId { get; set; }
    
    DateTime IEvent<TIdentity>.OccuredOn { get; set; }


    public TIdentity AggregateRootId
    {
        get => ((IEvent<TIdentity>)this).AggregateRootId;
        init => ((IEvent<TIdentity>)this).AggregateRootId = value;
    }

    public DateTime OccuredOn
    {
        get => ((IEvent<TIdentity>)this).OccuredOn; 
        init => ((IEvent<TIdentity>)this).OccuredOn = value;
    }
    
}