using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Hotels.Create;

public record AddHotelCommand(
    string Name,
    string Description,
    string Country,
    string State,
    string City,
    string ZipCode,
    string Street,
    bool? IsActive
    ) : ICommand<Guid>;