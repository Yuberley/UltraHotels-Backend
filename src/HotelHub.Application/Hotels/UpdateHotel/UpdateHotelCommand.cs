using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Hotels.UpdateHotel;

public record UpdateHotelCommand(
    Guid Id,
    string Name,
    string Description,
    string Country,
    string State,
    string City,
    string ZipCode,
    string Street,
    bool IsActive): ICommand<Guid>;