using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Rooms.CreateRoom;

public record AddRoomCommand(
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