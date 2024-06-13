using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Bookings.Events;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Domain.Bookings;

public sealed class Booking : Entity
{
    private Booking(
        Guid id,
        Guid roomId,
        Guid userId,
        DateRange duration,
        Taxes taxes,
        Money priceForPeriod,
        Money taxesPrice,
        Money totalPrice,
        BookingStatus status,
        EmergencyContactFullName emergencyContactFullName,
        PhoneNumber emergencyContactPhoneNumber,
        DateTime createdOnUtc)
        : base(id)
    {
        RoomId = roomId; 
        UserId = userId;
        Duration = duration;
        Taxes = taxes;
        PriceForPeriod = priceForPeriod;
        TaxesPrice = taxesPrice;
        TotalPrice = totalPrice; 
        Status = status;
        EmergencyContactFullName = emergencyContactFullName;
        EmergencyContactPhoneNumber = emergencyContactPhoneNumber;
        CreatedOnUtc = createdOnUtc;
    }
    
    // This empty constructor is necessary for Entity Framework,
    // they require a constructor without parameters for instance creation.
    private Booking()
    {
    }
    
    public Guid RoomId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public DateRange Duration { get; private set; }
    
    public Taxes Taxes { get; private set; }
    
    public Money PriceForPeriod { get; private set; }
    
    public Money TaxesPrice { get; private set; }
    
    public Money TotalPrice { get; private set; }
    
    public BookingStatus Status { get; private set; }
    
    public EmergencyContactFullName EmergencyContactFullName { get; private set; }
    
    public PhoneNumber EmergencyContactPhoneNumber { get; private set; }
    
    public DateTime CreatedOnUtc { get; private set; }
    
    
    public static Booking Reserve(
        Room room,
        Guid userId,
        DateRange duration,
        DateTime createdOnUtc,
        PricingService pricingService,
        EmergencyContactFullName emergencyContactFullName,
        PhoneNumber emergencyContactPhoneNumber
        )
    {
        PricingDetails pricingDetails = pricingService.CalculatePrice(room, duration);
        
        var booking = new Booking(
            Guid.NewGuid(),
            room.Id,
            userId,
            duration,
            room.Taxes,
            pricingDetails.PriceForPeriod,
            pricingDetails.TaxesPrice,
            pricingDetails.TotalPrice,
            BookingStatus.Reserved,
            emergencyContactFullName,
            emergencyContactPhoneNumber,
            createdOnUtc);
        
        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));
        
        return booking;
    }
    
}