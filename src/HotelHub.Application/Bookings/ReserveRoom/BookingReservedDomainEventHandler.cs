using HotelHub.Application.Abstractions.Email;
using HotelHub.Domain.Bookings;
using HotelHub.Domain.Bookings.Events;
using HotelHub.Domain.Users;
using MediatR;

namespace HotelHub.Application.Bookings.ReserveRoom;

internal sealed class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
{
    
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    
    public BookingReservedDomainEventHandler(IBookingRepository bookingRepository, IEmailService emailService, IUserRepository userRepository)
    {
        _bookingRepository = bookingRepository;
        _emailService = emailService;
        _userRepository = userRepository;
    }
    
    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        Booking? booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);
        
        if (booking is null)
        {
            return;
        }
        
        User? user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);
        
        if (user is null)
        {
            return;
        }
        
        await _emailService.SendAsync(
            user.Email,
            "Booking reserved!",
            "Thank you for booking with us!");
    }
}