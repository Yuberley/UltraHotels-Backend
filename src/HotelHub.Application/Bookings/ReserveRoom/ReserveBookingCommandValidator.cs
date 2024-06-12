using FluentValidation;

namespace HotelHub.Application.Bookings.ReserveRoom;

public class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        
        RuleFor(c => c.RoomId).NotEmpty();
        
        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}