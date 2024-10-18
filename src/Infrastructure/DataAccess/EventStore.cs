using Core.Domain.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Infrastructure.DataAccess;

public class EventStore(IMongoDatabase database) : IEventStore
{
    public async Task<List<IEvent<TIdentity>>> RetrieveEvents<TIdentity>(TIdentity aggregateRootId,
        CancellationToken cancellationToken = default) where TIdentity : AbstractIdentity
    {
        IMongoCollection<StoredEvent> collection = database.GetCollection<StoredEvent>("Events");
        IAsyncCursor<StoredEvent> cursor = await collection.FindAsync(@event => @event.AggregateRootId == aggregateRootId,
            cancellationToken: cancellationToken);
        List<StoredEvent> stagedEvents = await cursor.ToListAsync(cancellationToken: cancellationToken); 

        return stagedEvents.OrderBy(@event => @event.Position)
            .Select(@event => (IEvent<TIdentity>)@event.Event)
            .ToList();
    }

    public async Task AppendEvent<TIdentity>(IEvent<TIdentity> @event,
        CancellationToken cancellationToken = default) where TIdentity : AbstractIdentity
    {
        IMongoCollection<StoredEvent> collection = database.GetCollection<StoredEvent>("Events");

        int position = Sequence.GetNextSequenceValue("Events", database);

        var storedEvent = new StoredEvent
        {
            Id = Guid.NewGuid(),
            AggregateRootId = @event.AggregateRootId,
            Position = position,
            OccuredOn = @event.OccuredOn,
            Event = @event
        };

        await collection.InsertOneAsync(storedEvent, cancellationToken: cancellationToken);
    }
    
    internal class Sequence
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        public string Name { get; set; } = "Events";

        public int Value { get; set;  }

        public void Insert(IMongoDatabase database)
        {
            var collection = database.GetCollection<Sequence>("sequence");
            var filter = Builders<Sequence>.Filter.Eq("Name", Name);
            var update = Builders<Sequence>.Update.SetOnInsert("Name", Name)
                .SetOnInsert("Value", 1);

            var options = new UpdateOptions { IsUpsert = true };

            collection.UpdateOne(filter, update, options);
        }

        internal static int GetNextSequenceValue(string sequenceName, IMongoDatabase database)
        {
            var collection = database.GetCollection<Sequence>("sequence");
            var filter = Builders<Sequence>.Filter.Eq(a => a.Name, sequenceName);
            var update = Builders<Sequence>.Update.Inc(a => a.Value, 1);
            var sequence = collection.FindOneAndUpdate(filter, update);

            return sequence.Value;
        }
    }
}