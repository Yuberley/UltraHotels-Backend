using FluentValidation;

namespace HotelHub.Application.Rooms.Commands.Create;

public class AddRoomCommandValidator : AbstractValidator<AddRoomCommand>
{
    public AddRoomCommandValidator()
    {
        RuleFor(x => x.HotelId)
            .NotEmpty();
        RuleFor(x => x.RoomNumber)
            .NotEmpty();
        RuleFor(x => x.Type)
            .NotEmpty();
        RuleFor(x => x.NumberGuestsAdults)
            .NotEmpty();
        RuleFor(x => x.NumberGuestsChildren)
            .NotEmpty();
        RuleFor(x => x.BaseCost)
            .NotEmpty();
        RuleFor(x => x.Currency)
            .NotEmpty();
        RuleFor(x => x.Taxes)
            .NotEmpty();
    }
}