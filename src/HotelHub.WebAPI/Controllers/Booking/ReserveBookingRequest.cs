namespace HotelHub.WebAPI.Controllers.Booking;

public sealed record ReserveBookingRequest(
    Guid RoomId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate);