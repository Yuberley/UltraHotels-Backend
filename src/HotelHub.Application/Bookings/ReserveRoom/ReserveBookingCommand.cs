using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Bookings.ReserveRoom;

public record ReserveBookingCommand(
    Guid RoomId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate) : ICommand<Guid>;