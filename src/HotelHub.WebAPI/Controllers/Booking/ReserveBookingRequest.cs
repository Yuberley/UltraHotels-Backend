namespace HotelHub.WebAPI.Controllers.Booking;

public sealed record ReserveBookingRequest(
    Guid RoomId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    string EmergencyContactFullName,
    string EmergencyContactPhoneNumber,
    List<GuestRequest> Guests
    );

public record GuestRequest (
    string FirstName,
    string LastName,
    string Email,
    string TypeDocument,
    string Document,
    string Phone,
    string Gender,
    DateOnly BirthDate
);