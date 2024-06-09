namespace HotelHub.Domain.Abstractions;

public abstract class Entity
{
    
    private readonly List<IDomainEvent> _domainEvents = new();
    private Guid Id { get; init; }
    
    
    protected Entity(Guid id)
    {
        Id = id;
    }
    
    protected Entity()
    {
    }
    
    
    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
}