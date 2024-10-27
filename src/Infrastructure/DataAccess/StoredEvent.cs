using Core.Domain.Model;

namespace Infrastructure.DataAccess;

internal class StoredEvent
{
    public Guid Id { get; set; }
    
    public DateTime OccuredOn { get; set; }
    
    public long Position { get; set; }
    
    public string AggregateRootIdType { get; set; }
    
    public string AggregateRootId { get; set; }
    
    public string EventType { get; set; }
    
    public string Event { get; set; }
}