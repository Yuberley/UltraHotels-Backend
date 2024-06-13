using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Rooms.GetRooms;

public sealed record GetAllRoomsQuery : IQuery<IEnumerable<RoomResponse>>;