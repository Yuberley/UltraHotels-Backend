using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Rooms.SearchRooms;

public record SearchRoomsQuery(
    DateOnly StartDate,
    DateOnly EndDate,
    int NumberAdults,
    int NumberChildren,
    string Country) : IQuery<IReadOnlyList<RoomSearchResponse>>;