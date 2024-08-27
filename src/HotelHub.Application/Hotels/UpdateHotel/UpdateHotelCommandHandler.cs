using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Hotels;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Application.Hotels.UpdateHotel;

internal sealed class UpdateHotelCommandHandler : ICommandHandler<UpdateHotelCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHotelRepository _hotelRepository;
    
    public UpdateHotelCommandHandler(IUnitOfWork unitOfWork, IHotelRepository hotelRepository)
    {
        _unitOfWork = unitOfWork;
        _hotelRepository = hotelRepository;
    }
    
    public async Task<Result<Guid>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (hotel is null)
        {
            return Result.Failure<Guid>(HotelErrors.NotFound(request.Id));
        }
        
        var newHotel = new Hotel(
            request.Id,
            new Name(request.Name),
            new Description(request.Description),
            new Address(request.Country, request.State, request.City, request.ZipCode, request.Street),
            IsActive.FromValue(request.IsActive),
            hotel.CreatedAtOnUtc
            );
        
        _hotelRepository.Update(newHotel);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return newHotel.Id;
    }
}