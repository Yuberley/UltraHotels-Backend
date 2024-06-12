using HotelHub.Application.Abstractions.Clock;
using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Application.Exceptions;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Bookings;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.Users;

namespace HotelHub.Application.Bookings.ReserveRoom;

internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService _pricingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public ReserveBookingCommandHandler(
        IRoomRepository roomRepository, 
        IBookingRepository bookingRepository, 
        IUnitOfWork unitOfWork, 
        PricingService pricingService, 
        IDateTimeProvider dateTimeProvider, IUserRepository userRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
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
                _pricingService);
            
            _bookingRepository.Add(booking);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return booking.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }
        
    }
}