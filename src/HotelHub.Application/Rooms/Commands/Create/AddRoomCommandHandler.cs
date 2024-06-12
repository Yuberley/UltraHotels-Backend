using HotelHub.Application.Abstractions.Clock;
using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Hotels;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Application.Rooms.Commands.Create;

internal sealed class AddRoomCommandHandler : ICommandHandler<AddRoomCommand, Guid>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public AddRoomCommandHandler(IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork, IRoomRepository roomRepository, IHotelRepository hotelRepository)
    {
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
    }
    
    public async Task<Result<Guid>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
    {
        Hotel? hotel = await _hotelRepository.GetByIdAsync(request.HotelId, cancellationToken);
        
        if (hotel is null)
        {
            return Result.Failure<Guid>(HotelErrors.NotFound(request.HotelId));
        }
        
        var room = new Room(
            Guid.NewGuid(),
            request.HotelId,
            new RoomNumber(request.RoomNumber),
            new NumberGuests(request.NumberGuestsAdults, request.NumberGuestsChildren),
            new RoomType(request.Type),
            new Money(request.BaseCost, Currency.FromCode(request.Currency)),
            Taxes.FromValue(request.Taxes),
            IsActive.FromValue(request.IsActive),
            _dateTimeProvider.UtcNow
            );
        
        _roomRepository.Add(room);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return room.Id;
    }
}