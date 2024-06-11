using HotelHub.Domain.Abstractions;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Domain.Rooms;

public sealed class Room : Entity
{
    public Room(
        Guid id,
        Guid hotelId, 
        RoomNumber roomNumber,
        NumberGuests numberGuests,
        RoomType type,
        Money baseCost,
        Taxes taxes, 
        IsActive isActive,
        DateTime createdAtOnUtc) : base(id)
    {
        HotelId = hotelId;
        RoomNumber = roomNumber;
        NumberGuests = numberGuests;
        Type = type;
        BaseCost = baseCost;
        Taxes = taxes;
        IsActive = isActive;
        CreatedAtOnUtc = createdAtOnUtc;
    }

    // This empty constructor is necessary for Entity Framework,
    // they require a constructor without parameters for instance creation.
    private Room() {}

    public Guid HotelId { get; private set; }
    public RoomNumber RoomNumber { get; private set; }
    public NumberGuests NumberGuests { get; private set; }
    public RoomType Type { get; private set; }
    public Money BaseCost { get; private set; }
    public Taxes Taxes { get; private set; }
    public IsActive IsActive { get; private set; }
    public DateTime CreatedAtOnUtc { get; private set; }
    
    public void UpdateRoom(RoomNumber roomNumber, RoomType type, Money baseCost, Taxes taxes, IsActive isActive)
    {
        RoomNumber = roomNumber;
        Type = type;
        BaseCost = baseCost;
        Taxes = taxes;
        IsActive = isActive;
    }
}