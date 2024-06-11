using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Hotels.Queries.GetAll;

public sealed record GetAllHotelsQuery : IQuery<IEnumerable<HotelResponse>>;