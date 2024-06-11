using AutoMapper;
using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Rooms;

namespace HotelHub.Application.Rooms.Queries.GetAll;

internal sealed class GetAllRoomsHandler : IQueryHandler<GetAllRoomsQuery, IEnumerable<RoomResponse>>
{
    private readonly IRoomRepository _roomRepository;

    public GetAllRoomsHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<Result<IEnumerable<RoomResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.GetAllAsync(cancellationToken);
        var roomResponses = rooms.Select(room => new RoomResponse
        {
            Id = room.Id,
            HotelId = room.HotelId,
            RoomNumber = room.RoomNumber.Value,
            NumberGuests = new NumberGuestsResponse
            {
                Adults = room.NumberGuests.Adults,
                Children = room.NumberGuests.Children
            },
            Type = room.Type.Value,
            BaseCost = room.BaseCost.Amount,
            Currency = room.BaseCost.Currency.Code,
            IsActive = room.IsActive.Value,
            CreatedAtOnUtc = room.CreatedAtOnUtc
        });
        
        return Result.Success(roomResponses);
    }
}