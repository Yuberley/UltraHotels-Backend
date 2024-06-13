using FluentValidation;
using HotelHub.Domain.Guests;

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
        
        RuleForEach(c => c.Guests).SetValidator(new GuestCommandValidator());
    }
}

public class GuestCommandValidator : AbstractValidator<GuestCommand>
{
    public GuestCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(50);
        
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(50);
        
        RuleFor(c => c.Email).NotEmpty().EmailAddress().MaximumLength(100);
        
        RuleFor(c => c.TypeDocument)
            .NotEmpty()
            .Must(x => DocumentType.All.Any(c => c.Value == x))
            .WithMessage($"The document type value is invalid, only {string.Join(", ", DocumentType.All.Select(c => c.Value))} are allowed");
        
        RuleFor(c => c.Document).NotEmpty().MaximumLength(20);
        
        RuleFor(c => c.Phone).NotEmpty().MaximumLength(15);
        
        RuleFor(c => c.Gender)
            .NotEmpty()
            .Must(x => Gender.All.Any(c => c.Value == x))
            .WithMessage($"The gender value is invalid, only {string.Join(", ", Gender.All.Select(c => c.Value))} are allowed");
        
        RuleFor(c => c.BirthDate).NotEmpty();
    }
}