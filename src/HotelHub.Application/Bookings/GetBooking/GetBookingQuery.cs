using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>;