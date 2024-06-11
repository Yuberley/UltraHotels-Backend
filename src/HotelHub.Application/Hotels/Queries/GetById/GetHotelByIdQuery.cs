using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Hotels.Queries.GetById;

public sealed record GetHotelByIdQuery(Guid Id) : IQuery<HotelResponse>;