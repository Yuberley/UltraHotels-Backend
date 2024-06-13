using HotelHub.Application.Abstractions.Clock;
using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Application.Exceptions;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Bookings;
using HotelHub.Domain.Guests;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.SharedValueObjects;
using HotelHub.Domain.Users;

namespace HotelHub.Application.Bookings.ReserveRoom;

internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService _pricingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public ReserveBookingCommandHandler(
        IRoomRepository roomRepository, 
        IBookingRepository bookingRepository, 
        IUnitOfWork unitOfWork, 
        PricingService pricingService, 
        IDateTimeProvider dateTimeProvider, IUserRepository userRepository, IGuestRepository guestRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _guestRepository = guestRepository;
    }
    
    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }
        
        Room? room = await _roomRepository.GetByIdAsync(request.RoomId, cancellationToken);
        
        if (room is null)
        {
            return Result.Failure<Guid>(RoomErrors.NotFound);
        }
        
        int maxGuests = room.NumberGuests.Adults + room.NumberGuests.Children;
        if (request.Guests.Count > maxGuests)
        {
            return Result.Failure<Guid>(RoomErrors.ExcessiveGuests);
        }
        
        if (room.IsActive.Value == false)
        {
            return Result.Failure<Guid>(RoomErrors.RoomIsDisabled);
        }
        
        var duration = DateRange.Create(request.StartDate, request.EndDate);
        
        if (await _bookingRepository.IsOverlappingAsync(room, duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }
        
        try
        {
            var booking = Booking.Reserve(
                room,
                user.Id,
                duration,
                _dateTimeProvider.UtcNow,
                _pricingService,
                new EmergencyContactFullName(request.EmergencyContactFullName),
                PhoneNumber.Create(request.EmergencyContactPhoneNumber)
            );
            
            _bookingRepository.Add(booking);
            
            var guests = request.Guests.Select(g => new Guest(
                Guid.NewGuid(),
                booking.Id,
                new FirstName(g.FirstName),
                new LastName(g.LastName),
                new Email(g.Email),
                PhoneNumber.Create(g.Phone),
                new BirthDate(g.BirthDate),
                Gender.Create(g.Gender),
                DocumentType.Create(g.TypeDocument),
                new DocumentNumber(g.Document),
                _dateTimeProvider.UtcNow
                )).ToList();
            
                
            // void AddList(List<Guest> guests);
            _guestRepository.AddList(guests);
                
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return booking.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }
        
    }
}