using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Hotels.Queries.Search;

public sealed record SearchHotelQuery(
    DateOnly StartDate,
    DateOnly EndDate) : IQuery<IReadOnlyList<HotelResponse>>;