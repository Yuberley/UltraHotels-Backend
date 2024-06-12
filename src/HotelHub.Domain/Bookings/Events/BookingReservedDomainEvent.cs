using HotelHub.Domain.Abstractions;

namespace HotelHub.Domain.Bookings.Events;

public record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;