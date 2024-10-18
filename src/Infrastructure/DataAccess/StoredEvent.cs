using Core.Domain.Model;

namespace Infrastructure.DataAccess;

internal class StoredEvent
{
    public Guid Id { get; set; }
    
    public DateTime OccuredOn { get; set; }
    
    public int Position { get; set; }
    
    public AbstractIdentity AggregateRootId { get; set; }
    
    public IEvent Event { get; set; }
}