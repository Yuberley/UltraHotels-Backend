using HotelHub.Domain.Hotels;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.SharedValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Database.Configurations;

internal sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(room => room.Id);
        
        builder.Property(room => room.RoomNumber)
            .HasMaxLength(10);
            
        builder.OwnsOne(room => room.BaseCost, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });
        
        builder.Property(room => room.Taxes)
            .HasConversion(taxes => taxes.Value, value => new Taxes(value));
        
        builder.Property(room => room.IsActive);
        
        builder.HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(room => room.HotelId);
    }
}