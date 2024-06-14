using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Domain.Bookings;

public sealed record PricingDetails(
    Money PriceForPeriod,
    Money TaxesPrice,
    Money TotalPrice
    );