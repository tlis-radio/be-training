using MediatR;

namespace Core.Domain.Model;

public abstract class Entity<TIdentity> : IEntity<TIdentity> where TIdentity : AbstractIdentity
{
    private TIdentity? _identity;

    public TIdentity Identity
    {
        get => _identity ?? BusynessRuleException.AccessingUninitialisedState<TIdentity>();
        protected set => _identity = value ?? throw new BusynessRuleException("Identity cannot be null.");
    }
}