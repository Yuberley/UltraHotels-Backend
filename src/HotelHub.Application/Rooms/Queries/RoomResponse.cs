namespace HotelHub.Application.Rooms.Queries;

public sealed class RoomResponse
{
    public Guid Id { get; init; }
    public Guid HotelId { get; init; }
    public int RoomNumber { get; init; }
    public NumberGuestsResponse NumberGuests { get; init; }
    public string Type { get; init; }
    public decimal BaseCost { get; init; }
    public string Currency { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAtOnUtc { get; init; }
}