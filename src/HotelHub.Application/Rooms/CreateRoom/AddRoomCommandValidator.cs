using FluentValidation;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Application.Rooms.CreateRoom;

public class AddRoomCommandValidator : AbstractValidator<AddRoomCommand>
{
    public AddRoomCommandValidator()
    {
        RuleFor(x => x.HotelId).NotEmpty();
        RuleFor(x => x.RoomNumber).NotEmpty();
        
        RuleFor(x => x.Type).Must(x => RoomType.All.Any(c => c.Value == x)).WithMessage("The room type value is invalid");
        
        RuleFor(x => x.NumberGuestsAdults).NotEmpty().GreaterThan(0).WithMessage("The number of adults must be greater than 0");
        RuleFor(x => x.NumberGuestsChildren).GreaterThanOrEqualTo(0).WithMessage("The number of children must be greater than or equal to 0");
        RuleFor(x => x.BaseCost).NotEmpty().GreaterThan(0).WithMessage("The base cost must be greater than 0");
        RuleFor(x => x.Currency).Must(x => Currency.All.Any(c => c.Code == x)).WithMessage($"The currency code is invalid, olny {string.Join(", ", Currency.All.Select(c => c.Code))} are allowed");
        RuleFor(x => x.Taxes).InclusiveBetween(0, 100).NotEmpty();
        RuleFor(x => x.IsActive).NotNull();
    }
}