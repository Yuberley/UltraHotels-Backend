using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Bookings;

namespace HotelHub.Application.Bookings.GetBooking;

public class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, BookingResponse>
{
    private readonly IBookingRepository _bookingRepository;
    
    public GetBookingQueryHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    
    
    public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        Booking? booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);
        
        if (booking is null)
        {
            return Result.Failure<BookingResponse>(BookingErrors.NotFound);
        }
        
        return Result.Success(new BookingResponse
        {
            Id = booking.Id,
            UserId = booking.UserId,
            RoomId = booking.RoomId,
            Status = (int)booking.Status,
            PriceAmount = booking.PriceForPeriod.Amount,
            PriceCurrency = booking.PriceForPeriod.Currency.Code,
            TaxesPercentage = booking.Taxes.Value,
            TaxesPriceAmount = booking.TaxesPrice.Amount,
            TaxesPriceCurrency = booking.TaxesPrice.Currency.Code,
            TotalPriceAmount = booking.TotalPrice.Amount,
            TotalPriceCurrency = booking.TotalPrice.Currency.Code,
            DurationStart = booking.Duration.Start,
            DurationEnd = booking.Duration.End,
            CreatedOnUtc = booking.CreatedOnUtc
        });
    }
}