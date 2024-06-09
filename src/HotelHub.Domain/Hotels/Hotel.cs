using HotelHub.Domain.Abstractions;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Domain.Hotels;

public sealed class Hotel : Entity
{
    
    public Hotel(
        Guid id,
        Name name, 
        Description description, 
        Address address, 
        IsActive isActive, 
        DateTime createdAtOnUtc, 
        DateTime updatedAtOnUtc) : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        IsActive = isActive;
        CreatedAtOnUtc = createdAtOnUtc;
        UpdatedAtOnUtc = updatedAtOnUtc;
    }
    
    // This empty constructor is necessary for Entity Framework,
    // they require a constructor without parameters for instance creation.
    protected Hotel() {}
    
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Address Address { get; private set; }
    public IsActive IsActive { get; private set; }
    public DateTime CreatedAtOnUtc { get; private set; }
    public DateTime UpdatedAtOnUtc { get; private set; }
 
}