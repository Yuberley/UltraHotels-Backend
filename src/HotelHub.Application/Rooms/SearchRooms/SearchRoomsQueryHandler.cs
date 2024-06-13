using System.Data;
using Dapper;
using HotelHub.Application.Abstractions.Data;
using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;

namespace HotelHub.Application.Rooms.SearchRooms;

internal sealed class SearchRoomsQueryHandler : IQueryHandler<SearchRoomsQuery, IReadOnlyList<RoomSearchResponse>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    
    public SearchRoomsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public async Task<Result<IReadOnlyList<RoomSearchResponse>>> Handle(SearchRoomsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<RoomSearchResponse>();
        }
        
        using IDbConnection connection = _dbConnectionFactory.CreateConnection();
        
        // TODO: Check the "TypeRoom" and "BaseCostCurrency" fields, because this field returns null.
        string sql = $@"
                SELECT
                    r.id AS Id,
                    r.hotel_id AS HotelId,
                    r.room_number AS RoomNumber,
                    r.number_guests_adults AS Adults,
                    r.number_guests_children AS Children,
                    r.type AS TypeRoom,
                    r.base_cost_amount AS BaseCost,
                    r.base_cost_currency AS BaseCostCurrency,
                    r.taxes AS Taxes,
                    r.is_active AS IsActive,
                    r.created_at_on_utc AS CreatedAtOnUtc,
                    h.name AS HotelName,
                    h.description AS HotelDescription,
                    h.address_country AS HotelCountry,
                    h.address_state AS HotelState,
                    h.address_city AS HotelCity,
                    h.address_street AS HotelStreet
                FROM rooms AS r
                INNER JOIN hotels AS h ON r.hotel_id = h.id
                WHERE r.is_active = true
                  AND r.number_guests_adults >= @NumberAdults
                  AND r.number_guests_children >= @NumberChildren
                  AND h.address_country = @Country
                  AND r.id NOT IN (
                    SELECT b.room_id
                    FROM bookings AS b
                    WHERE b.duration_start <= @EndDate
                    AND b.duration_end >= @StartDate
                )
            ";
        
        var parameters = new
        {
            StartDate = request.StartDate.ToDateTime(TimeOnly.MinValue),
            EndDate = request.EndDate.ToDateTime(TimeOnly.MaxValue),
            NumberAdults = request.NumberAdults,
            NumberChildren = request.NumberChildren,
            Country = request.Country
        };
        
        IEnumerable<RoomSearchResponse> rooms = await connection
            .QueryAsync<RoomSearchResponse, NumberGuestsResponse, HotelResponse, RoomSearchResponse>(
                sql,
                (room, numberGuests, hotel) =>
                {
                    room.NumberGuests = numberGuests;
                    room.Hotel = hotel;
                    return room;
                },
                parameters,
                splitOn: "Adults,HotelName");
        
        return Result.Success<IReadOnlyList<RoomSearchResponse>>(rooms.ToList());
    }
}