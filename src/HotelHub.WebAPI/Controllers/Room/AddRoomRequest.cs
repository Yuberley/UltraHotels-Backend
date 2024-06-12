namespace HotelHub.WebAPI.Controllers.Room;

public record AddRoomRequest(
    Guid HotelId,
    int RoomNumber,
    string Type,
    int NumberGuestsAdults,
    int NumberGuestsChildren,
    decimal BaseCost,
    string Currency,
    decimal Taxes,
    bool IsActive);