using FluentValidation;

namespace HotelHub.Application.Rooms.SearchRooms;

public class SearchRoomQueryValidator : AbstractValidator<SearchRoomsQuery>
{
    public SearchRoomQueryValidator()
    {
        RuleFor(c => c.StartDate).NotEmpty();
        
        RuleFor(c => c.EndDate).NotEmpty();
        
        RuleFor(c => c.NumberAdults).GreaterThan(0);
        
        RuleFor(c => c.NumberChildren).GreaterThanOrEqualTo(0);
        
        RuleFor(c => c.Country).NotEmpty();
    }  
}