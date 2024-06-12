namespace HotelHub.WebAPI.Controllers.Room;

public sealed record SearchRoomsRequest(    
    DateOnly StartDate,
    DateOnly EndDate);