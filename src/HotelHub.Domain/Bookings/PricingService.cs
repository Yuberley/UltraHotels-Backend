using HotelHub.Domain.Rooms;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Domain.Bookings;

public class PricingService
{
    public PricingDetails CalculatePrice(Room room, DateRange duration)
    {
        var priceForPeriod = new Money(
            room.BaseCost.Amount * duration.LengthInDays,
            room.BaseCost.Currency);

        var taxes = new Money(
            priceForPeriod.Amount * room.Taxes.Value / 100,
            room.BaseCost.Currency);

        var totalPrice = Money.Zero(room.BaseCost.Currency);
        totalPrice += priceForPeriod;
        totalPrice += taxes;

        return new PricingDetails(priceForPeriod, taxes, totalPrice);
    }
}