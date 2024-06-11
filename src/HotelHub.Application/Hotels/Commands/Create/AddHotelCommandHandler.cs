using HotelHub.Application.Abstractions.Clock;
using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Hotels;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Application.Hotels.Commands.Create;


internal sealed class AddHotelCommandHandler : ICommandHandler<AddHotelCommand, Guid>
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHotelRepository _hotelRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    
    public AddHotelCommandHandler(IUnitOfWork unitOfWork, IHotelRepository hotelRepository, IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _hotelRepository = hotelRepository;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<Result<Guid>> Handle(AddHotelCommand request, CancellationToken cancellationToken)
    {
        
        IsActive isActive = request.IsActive.HasValue && request.IsActive.Value
            ? IsActive.Yes
            : IsActive.No;
        
        var hotel = new Hotel(
            Guid.NewGuid(),
            new Name(request.Name),
            new Description(request.Description),
            new Address(request.Country, request.State, request.City, request.ZipCode, request.Street),
            isActive,
            _dateTimeProvider.UtcNow
            );
      
        _hotelRepository.Add(hotel);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return hotel.Id;
    }
}