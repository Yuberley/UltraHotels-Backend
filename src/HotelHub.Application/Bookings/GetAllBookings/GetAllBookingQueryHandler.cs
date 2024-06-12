using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Bookings;

namespace HotelHub.Application.Bookings.GetAllBookings;

public class GetAllBookingQueryHandler : IQueryHandler<GetAllBookingQuery, IEnumerable<BookingsResponse>>
{
    private readonly IBookingRepository _bookingRepository;
    
    public GetAllBookingQueryHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    
    public async Task<Result<IEnumerable<BookingsResponse>>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _bookingRepository.GetAllAsync(cancellationToken);
        var bookingResponses = bookings.Select(booking => new BookingsResponse
        {
            Id = booking.Id,
            UserId = booking.UserId,
            RoomId = booking.RoomId,
            Status = (int)booking.Status,
            PriceAmount = booking.PriceForPeriod.Amount,
            PriceCurrency = booking.PriceForPeriod.Currency.Code,
            TaxesPriceAmount = booking.TaxesPrice.Amount,
            TaxesPriceCurrency = booking.TaxesPrice.Currency.Code,
            TotalPriceAmount = booking.TotalPrice.Amount,
            TotalPriceCurrency = booking.TotalPrice.Currency.Code,
            DurationStart = booking.Duration.Start,
            DurationEnd = booking.Duration.End,
            CreatedOnUtc = booking.CreatedOnUtc
        });
        
        return Result.Success(bookingResponses);
    }
}