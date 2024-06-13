using FluentValidation;

namespace HotelHub.Application.Rooms.UpdateRoom;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.RoomId).NotEmpty();
        RuleFor(x => x.RoomNumber).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.NumberGuestsAdults).NotEmpty().GreaterThan(0);
        RuleFor(x => x.NumberGuestsChildren).GreaterThanOrEqualTo(0);
        RuleFor(x => x.BaseCost).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Currency).NotEmpty();
        RuleFor(x => x.Taxes).InclusiveBetween(0, 100).NotEmpty();
    }
}