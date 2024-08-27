using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Bookings.ReserveRoom;

public record ReserveBookingCommand(
    Guid RoomId,
    DateOnly StartDate,
    DateOnly EndDate,
    string EmergencyContactFullName,
    string EmergencyContactPhoneNumber,
    List<GuestCommand> Guests
    ) : ICommand<Guid>;
    
public record GuestCommand (
    string FirstName,
    string LastName,
    string Email,
    string TypeDocument,
    string Document,
    string Phone,
    string Gender,
    DateOnly BirthDate
);