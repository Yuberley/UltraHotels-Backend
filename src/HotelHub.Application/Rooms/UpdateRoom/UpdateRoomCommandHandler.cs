using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Hotels;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.SharedValueObjects;
using MediatR;

namespace HotelHub.Application.Rooms.UpdateRoom;

internal sealed class UpdateRoomCommandHandler : ICommandHandler<UpdateRoomCommand, Guid>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateRoomCommandHandler(IRoomRepository roomRepository, IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        Hotel? hotel = await _hotelRepository.GetByIdAsync(request.HotelId, cancellationToken);
        
        if (hotel is null)
        {
            return Result.Failure<Guid>(HotelErrors.NotFound(request.HotelId));
        }
        
        Room? room = await _roomRepository.GetByIdAsync(request.RoomId, cancellationToken);
        
        if (room is null)
        {
            return Result.Failure<Guid>(RoomErrors.NotFound);
        }
        
        var newRoom = new Room(
            request.RoomId,
            request.HotelId,
            new RoomNumber(request.RoomNumber),
            new NumberGuests(request.NumberGuestsAdults, request.NumberGuestsChildren),
            new RoomType(request.Type),
            new Money(request.BaseCost, Currency.FromCode(request.Currency)),
            Taxes.FromValue(request.Taxes),
            IsActive.FromValue(request.IsActive),
            room.CreatedAtOnUtc
            );
        
        _roomRepository.Update(newRoom);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return newRoom.Id;
    }
}