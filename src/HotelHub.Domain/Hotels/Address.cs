namespace HotelHub.Domain.Hotels;

public sealed record Address(
    string Country,
    string State,
    string City,
    string ZipCode,
    string Street);