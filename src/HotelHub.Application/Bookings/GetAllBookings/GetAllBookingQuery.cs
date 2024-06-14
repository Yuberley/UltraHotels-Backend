using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Bookings.GetAllBookings;

public sealed record GetAllBookingQuery : IQuery<IEnumerable<BookingsResponse>>;