namespace HotelHub.WebAPI.Controllers.Hotel;

public record AddHotelRequest(
    string Name,
    string Description,
    string Country,
    string State,
    string City,
    string ZipCode,
    string Street,
    bool IsActive
);