using HotelHub.Domain.Bookings;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.SharedValueObjects;
using HotelHub.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Database.Configurations;

internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");
        
        builder.HasKey(booking => booking.Id);
        
        builder.OwnsOne(booking => booking.Duration);
        
        builder.OwnsOne(booking => booking.PriceForPeriod, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(
                    currency => currency.Code, 
                    code => Currency.FromCode(code));
        });
        
        builder.Property(booking => booking.Taxes)
            .HasConversion(
                taxes => taxes.Value,
                value => Taxes.FromValue(value))
            .HasColumnType("decimal(5, 2)");
        
        builder.OwnsOne(booking => booking.TaxesPrice, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(
                    currency => currency.Code, 
                    code => Currency.FromCode(code));
        });
        
        builder.OwnsOne(booking => booking.TotalPrice, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(
                    currency => currency.Code, 
                    code => Currency.FromCode(code));
        });
        
        builder.Property(booking => booking.Status)
            .HasConversion<int>();
        
        builder.Property(booking => booking.EmergencyContactPhoneNumber)
            .HasConversion(
                ecpn => ecpn.Value,
                value => PhoneNumber.Create(value))
            .HasMaxLength(15);
        
        builder.Property(booking => booking.EmergencyContactFullName)
            .HasConversion(
                ecfn => ecfn.Value,
                value => new EmergencyContactFullName(value))
            .HasMaxLength(200);
        
        builder.Property(booking => booking.CreatedOnUtc)
            .HasDefaultValueSql("NOW()");
        
        
        builder.HasOne<Room>()
            .WithMany()
            .HasForeignKey(booking => booking.RoomId);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(booking => booking.UserId);
    }
}