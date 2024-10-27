using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class EventStoreDbContext(DbContextOptions<EventStoreDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    internal DbSet<StoredEvent> StoredEvents { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StoredEvent>()
            .HasKey(@event => @event.Id);
        modelBuilder.Entity<StoredEvent>()
            .Property(@event => @event.OccuredOn)
            .IsRequired();
        modelBuilder.Entity<StoredEvent>()
            .Property(@event => @event.Position)
            .ValueGeneratedOnAdd()
            .IsRequired();
        modelBuilder.Entity<StoredEvent>()
            .HasIndex(@event => @event.Position)
            .IsUnique();
        modelBuilder.Entity<StoredEvent>()
            .Property(@event => @event.AggregateRootIdType)
            .IsRequired();
        modelBuilder.Entity<StoredEvent>()
            .Property(@event => @event.AggregateRootId)
            .IsRequired();
        modelBuilder.Entity<StoredEvent>()
            .Property(@event => @event.EventType)
            .IsRequired();
        modelBuilder.Entity<StoredEvent>()
            .Property(@event => @event.Event)
            .IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}