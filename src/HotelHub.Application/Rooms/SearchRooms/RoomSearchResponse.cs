namespace HotelHub.Application.Rooms.SearchRooms;

public sealed class RoomSearchResponse
{
    public Guid Id { get; init; }
    public Guid HotelId { get; init; }
    public HotelResponse Hotel { get; set; }
    public int RoomNumber { get; init; }
    public NumberGuestsResponse NumberGuests { get; set; }
    public string TypeRoom { get; init; }
    public decimal BaseCost { get; init; }
    public string BaseCostCurrency { get; init; }
    public decimal Taxes { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAtOnUtc { get; init; }
}

public sealed class HotelResponse
{
    public string HotelName { get; init; }
    public string HotelDescription { get; init; }
    public string HotelCountry { get; init; }
    public string HotelState { get; init; }
    public string HotelCity { get; init; }
    public string HotelStreet { get; init; }
}

public sealed class NumberGuestsResponse
{
    public int Adults { get; init; }
    public int Children { get; init; }
}