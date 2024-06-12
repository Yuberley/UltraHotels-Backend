using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Rooms.Queries.SearchRooms;

public record SearchRoomsQuery(
    DateOnly StartDate,
    DateOnly EndDate) : IQuery<IEnumerable<RoomResponse>>;