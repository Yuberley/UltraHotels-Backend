using FluentValidation;

namespace HotelHub.Application.Bookings.ReserveRoom;

public class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        
        RuleFor(c => c.RoomId).NotEmpty();
        
        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
        
        RuleFor(c => c.EmergencyContactFullName).NotEmpty();
        
        RuleFor(c => c.EmergencyContactPhoneNumber).NotEmpty();
        
        RuleFor(c => c.Guests).NotEmpty();
        
    }
}


// public record ReserveBookingCommand(
//     Guid RoomId,
//     Guid UserId,
//     DateOnly StartDate,
//     DateOnly EndDate,
//     string EmergencyContactFullName,
//     string EmergencyContactPhoneNumber,
//     List<GuestCommand> Guests
// ) : ICommand<Guid>;
//
// public record GuestCommand (
//     string FirstName,
//     string LastName,
//     string Email,
//     string TypeDocument,
//     string Document,
//     string Phone,
//     string Gender,
//     DateOnly BirthDate
// );