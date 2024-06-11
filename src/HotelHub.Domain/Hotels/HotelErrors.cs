using HotelHub.Domain.Abstractions;

namespace HotelHub.Domain.Hotels;

public class HotelErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "Users.NotFound", 
        $"The hotel with the Id = '{id}' was not found");
}