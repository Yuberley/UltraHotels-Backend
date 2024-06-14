namespace HotelHub.Application.Bookings.GetBooking;

public class BookingResponse
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    
    public Guid RoomId { get; init; }
    
    public int Status { get; init; }
    
    public decimal PriceAmount { get; init; }
    
    public string PriceCurrency { get; init; }
    
    public decimal TaxesPercentage { get; init; }
    
    public decimal TaxesPriceAmount { get; init; }
    
    public string TaxesPriceCurrency { get; init; }
    
    public decimal TotalPriceAmount { get; init; }
    
    public string TotalPriceCurrency { get; init; }
    
    public DateOnly DurationStart { get; init; }
    
    public DateOnly DurationEnd { get; init; }
    
    public DateTime CreatedOnUtc { get; init; }
}