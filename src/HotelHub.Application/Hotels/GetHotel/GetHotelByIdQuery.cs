using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Hotels.GetHotel;

public sealed record GetHotelByIdQuery(Guid Id) : IQuery<HotelResponse>;