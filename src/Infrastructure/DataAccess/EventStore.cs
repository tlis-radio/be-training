using System.Text.Json;
using Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DataAccess;

public class EventStore(EventStoreDbContext dbContext) : IEventStore
{
    public Task<List<IEvent<TIdentity>>> RetrieveEvents<TIdentity>(TIdentity aggregateRootId, CancellationToken cancellationToken = default) where TIdentity : AbstractIdentity
    {
        string aggregateRootIdJson = JsonSerializer.Serialize(aggregateRootId);
        List<StoredEvent> storedEvents = dbContext.StoredEvents.AsNoTracking()
            .Where(@event => @event.AggregateRootIdType == typeof(TIdentity).AssemblyQualifiedName)
            .Where(@event => @event.AggregateRootId == aggregateRootIdJson)
            .OrderBy(@event => @event.Position)
            .ToList();
        
        List<IEvent<TIdentity>> events = storedEvents.Select(@event =>
        {
            var eventType = Type.GetType(@event.EventType)!;
            return (IEvent<TIdentity>)JsonSerializer.Deserialize(@event.Event, eventType)!;;
        }).ToList();

        return Task.FromResult(events);
    }
    

    public async Task AppendEvent<TIdentity>(IEvent<TIdentity> @event, CancellationToken cancellationToken = default) where TIdentity : AbstractIdentity
    {
        string aggregateIdType = typeof(TIdentity).AssemblyQualifiedName!;
        string aggregateIdJson = JsonSerializer.Serialize(@event.AggregateRootId);
        string eventType = @event.GetType().AssemblyQualifiedName!;
        string eventJson = JsonSerializer.Serialize<object>(@event);

        var storedEvent = new StoredEvent
        {
            Id = Guid.NewGuid(),
            OccuredOn = @event.OccuredOn,
            AggregateRootId = aggregateIdJson,
            AggregateRootIdType = aggregateIdType,
            Event = eventJson,
            EventType = eventType
        };

        await dbContext.StoredEvents.AddAsync(storedEvent, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}