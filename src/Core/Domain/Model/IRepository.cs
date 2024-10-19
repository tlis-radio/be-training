using MediatR;

namespace Core.Domain.Model;

public interface IRepository<TAggregateRoot, TIdentity>
    where TAggregateRoot : IAggregateRoot<TIdentity>
    where TIdentity : AbstractIdentity
{
    public Task<TAggregateRoot> GetById(TIdentity identity, CancellationToken cancellationToken = default);

    public Task<bool> Exists(TIdentity identity, CancellationToken cancellationToken = default);
}