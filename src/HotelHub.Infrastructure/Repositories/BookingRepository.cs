using HotelHub.Domain.Bookings;
using HotelHub.Domain.Rooms;
using HotelHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public BookingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public Task<List<Booking>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Bookings.ToListAsync(cancellationToken);
    }
    
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }
    
    public Task<bool> IsOverlappingAsync(
        Room room, 
        DateRange duration, 
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Bookings
            .AnyAsync(
                booking =>
                    booking.RoomId == room.Id &&
                    booking.Duration.Start <= duration.End &&
                    booking.Duration.End >= duration.Start &&
                    ActiveBookingStatuses.Contains(booking.Status),
                cancellationToken);
    }
    
    public void Add(Booking booking)
    {
        _dbContext.Bookings.Add(booking);
    }
    
    
    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed
    };
}