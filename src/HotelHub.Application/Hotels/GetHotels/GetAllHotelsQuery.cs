using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Hotels.GetHotels;

public sealed record GetAllHotelsQuery : IQuery<IEnumerable<HotelResponse>>;