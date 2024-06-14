using HotelHub.Application.Abstractions.Email;
using HotelHub.Domain.Bookings;
using HotelHub.Domain.Bookings.Events;
using HotelHub.Domain.Hotels;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.Users;
using MediatR;

namespace HotelHub.Application.Bookings.ReserveRoom;

internal sealed class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
{
    
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    
    public BookingReservedDomainEventHandler(IBookingRepository bookingRepository, IEmailService emailService, IUserRepository userRepository, IRoomRepository roomRepository, IHotelRepository hotelRepository)
    {
        _bookingRepository = bookingRepository;
        _emailService = emailService;
        _userRepository = userRepository;
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
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
        
        Room? room = await _roomRepository.GetByIdAsync(booking.RoomId, cancellationToken);
        
        if (room is null)
        {
            return;
        }
        
        Hotel? hotel = await _hotelRepository.GetByIdAsync(room.HotelId, cancellationToken);
        
        if (hotel is null)
        {
            return;
        }
        
        string body = $"Hello 🙃\n\n" +
                      $"Thank you for booking with us!\n\n" +
                      $"Booking details:\n" +
                        $"- Hotel: {hotel.Name.Value}\n" +
                        $"- Room: {room.RoomNumber.Value}\n" +
                        $"- Check-in: {booking.Duration.Start}\n" +
                        $"- Check-out: {booking.Duration.End}\n" + 
                        $"- Price: {booking.TotalPrice.Amount:F2} {booking.TotalPrice.Currency.Code}\n\n" +
                        $"We hope you enjoy your stay!";
        
        try
        {
            await _emailService.SendAsync(
                user.Email,
                "Thank you for booking with us!",
                body);
        }
        catch (Exception)
        {
            // Log the exception
        }
        
    }
}