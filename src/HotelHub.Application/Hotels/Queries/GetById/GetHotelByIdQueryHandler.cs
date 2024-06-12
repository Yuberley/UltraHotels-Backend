using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Application.Hotels.Queries.GetAll;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Hotels;

namespace HotelHub.Application.Hotels.Queries.GetById;

internal sealed class GetHotelByIdQueryHandler : IQueryHandler<GetHotelByIdQuery, HotelResponse>
{
    private readonly IHotelRepository _hotelRepository;
    
    public GetHotelByIdQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }
    
    public async Task<Result<HotelResponse>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (hotel is null)
        {
            return Result.Failure<HotelResponse>(HotelErrors.NotFound(request.Id));
        }
        
        var response = new HotelResponse
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
        };
        
        return Result.Success(response);
    }
}