using HotelHub.Domain.Rooms;

namespace HotelHub.Domain.Bookings;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<bool> IsOverlappingAsync(
        Room room,
        DateRange duration,
        CancellationToken cancellationToken = default);
    
    void Add(Booking booking);
}