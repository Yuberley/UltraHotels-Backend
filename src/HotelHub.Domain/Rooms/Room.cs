using HotelHub.Domain.Abstractions;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Domain.Rooms;

public sealed class Room : Entity
{
    public Room(
        Guid id,
        Guid hotelId, 
        RoomNumber roomNumber, 
        RoomType type, 
        Money baseCost, 
        Money taxes, 
        IsActive isActive) : base(id)
    {
        HotelId = hotelId;
        RoomNumber = roomNumber;
        Type = type;
        BaseCost = baseCost;
        Taxes = taxes;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // This empty constructor is necessary for Entity Framework,
    // they require a constructor without parameters for instance creation.
    protected Room() {}

    public Guid HotelId { get; private set; }
    public RoomNumber RoomNumber { get; private set; }
    public RoomType Type { get; private set; }
    public Money BaseCost { get; private set; }
    public Money Taxes { get; private set; }
    public IsActive IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public void UpdateRoom(RoomNumber roomNumber, RoomType type, Money baseCost, Money taxes, IsActive isActive)
    {
        RoomNumber = roomNumber;
        Type = type;
        BaseCost = baseCost;
        Taxes = taxes;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void DeactivateRoom()
    {
        IsActive = IsActive.No;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void ActivateRoom()
    {
        IsActive = IsActive.Yes;
        UpdatedAt = DateTime.UtcNow;
    }
}