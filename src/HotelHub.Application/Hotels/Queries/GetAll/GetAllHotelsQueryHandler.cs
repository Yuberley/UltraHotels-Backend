using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Hotels;

namespace HotelHub.Application.Hotels.Queries.GetAll;

internal sealed class GetAllHotelsQueryHandler : IQueryHandler<GetAllHotelsQuery, IEnumerable<HotelResponse>>
{
    
    private readonly IHotelRepository _hotelRepository;
    
    public GetAllHotelsQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }
    
    public async Task<Result<IEnumerable<HotelResponse>>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        var hotels = await _hotelRepository.GetAllAsync(cancellationToken);
        var hotelResponses = hotels.Select(hotel => new HotelResponse
        {
            Id = hotel.Id,
            Name = hotel.Name.Value,
            Description = hotel.Description.Value,
            Address = new AddressResponse
            {
                Country = hotel.Address.Country,
                State = hotel.Address.State,
                City = hotel.Address.City,
                ZipCode = hotel.Address.ZipCode,
                Street = hotel.Address.Street
            },
            IsActive = hotel.IsActive.Value
        });
        
        return Result.Success(hotelResponses);
    }
}