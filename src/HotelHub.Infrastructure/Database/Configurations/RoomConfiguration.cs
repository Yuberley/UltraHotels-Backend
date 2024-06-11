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
        builder.ToTable("rooms");
        
        builder.HasKey(room => room.Id);
        
        builder.Property(room => room.RoomNumber)
            .HasConversion(roomNumber => roomNumber.Value, value => new RoomNumber(value))
            .HasMaxLength(10);
            
        builder.OwnsOne(room => room.NumberGuests, numberGuestsBuilder =>
        {
            numberGuestsBuilder.Property(numberGuests => numberGuests.Adults);
            numberGuestsBuilder.Property(numberGuests => numberGuests.Children);
        });
        
        builder.Property(room => room.Type)
            .HasConversion(
                roomType => roomType.Value, // Convertir a string para almacenar en la base de datos
                value => RoomType.FromValue(value)) // Convertir de string a RoomType al leer desde la base de datos
            .HasMaxLength(50);
        
        builder.OwnsOne(room => room.BaseCost, costBuilder =>
        {
            costBuilder.Property(cost => cost.Currency)
                .HasConversion(
                    currency => currency.Code,
                    code => Currency.FromCode(code));
        });
        
        builder.Property(room => room.Taxes)
            .HasConversion(
                taxes => taxes.Value, 
                value => Taxes.FromValue(value))
            .HasColumnType("decimal(5, 2)");
            
            
        builder.Property(room => room.IsActive)
            .HasConversion(
                isActive => isActive.Value,
                value => IsActive.Assign(value))
            .HasDefaultValue(IsActive.Default);
        
        builder.Property(room => room.CreatedAtOnUtc)
            .HasDefaultValueSql("NOW()");
        
        builder.HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(room => room.HotelId);
    }
}