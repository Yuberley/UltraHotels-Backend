using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Bookings;
using HotelHub.Domain.Rooms;

namespace HotelHub.Application.Rooms.Queries.SearchRooms;

internal sealed class SearchRoomsQueryHandler : IQueryHandler<SearchRoomsQuery, IEnumerable<RoomResponse>>
{
    private readonly IRoomRepository _roomRepository;
    
    public SearchRoomsQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<Result<IEnumerable<RoomResponse>>> Handle(SearchRoomsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<RoomResponse>();
        }
        
        const string sql = @"
            SELECT
                *
            FROM rooms AS r
            WHERE r.is_active = true
            AND r.id NOT IN (
                SELECT room_id
                FROM bookings
                WHERE duration_start <= @EndDate
                AND duration_end >= @StartDate
            )
        ";
            
        var rooms = await _roomRepository.SearchAsync(sql, cancellationToken);
        
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
            BaseCostCurrency = room.BaseCost.Currency.Code,
            Taxes = room.Taxes.Value,
            IsActive = room.IsActive.Value,
            CreatedAtOnUtc = room.CreatedAtOnUtc
        });
        
        return Result.Success(roomResponses);
    }
}