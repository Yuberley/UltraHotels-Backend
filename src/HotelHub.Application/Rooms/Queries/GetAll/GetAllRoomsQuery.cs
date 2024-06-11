using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Rooms.Queries.GetAll;

public sealed record GetAllRoomsQuery : IQuery<IEnumerable<RoomResponse>>;