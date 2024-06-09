namespace HotelHub.Domain.Abstractions;


/// <summary>
/// This is an abstract class that provides a base structure for all entities in the domain.
/// </summary>
/// <remarks>
/// This class is responsible for managing domain events.
/// </remarks>
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