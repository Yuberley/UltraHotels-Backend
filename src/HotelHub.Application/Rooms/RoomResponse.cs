namespace HotelHub.Application.Rooms;

public sealed class RoomResponse
{
    public Guid Id { get; init; }
    public Guid HotelId { get; init; }
    public int RoomNumber { get; init; }
    public NumberGuestsResponse NumberGuests { get; set; }
    public string Type { get; init; }
    public decimal BaseCost { get; init; }
    public string BaseCostCurrency { get; init; }
    public decimal Taxes { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAtOnUtc { get; init; }
}

public sealed class NumberGuestsResponse
{
    public int Adults { get; init; }
    public int Children { get; init; }
}