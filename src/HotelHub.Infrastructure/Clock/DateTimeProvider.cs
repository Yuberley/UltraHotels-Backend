using HotelHub.Application.Abstractions.Clock;

namespace HotelHub.Infrastructure.Clock;

internal sealed class DateTimeProvider  : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}