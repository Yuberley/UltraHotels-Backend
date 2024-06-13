using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Rooms.UpdateRoom;

public record UpdateRoomCommand(
    Guid RoomId,
    Guid HotelId,
    int RoomNumber,
    string Type,
    int NumberGuestsAdults,
    int NumberGuestsChildren,
    decimal BaseCost,
    string Currency,
    decimal Taxes,
    bool IsActive
    ) : ICommand<Guid>;