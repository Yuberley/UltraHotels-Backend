using HotelHub.Domain.Hotels;
using HotelHub.Domain.SharedValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Database.Configurations;

internal sealed class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("hotels");
        
        builder.HasKey(hotel => hotel.Id);
        
        builder.Property(hotel => hotel.Name)
            .HasConversion(
                name => name.Value,
                value => new Name(value))
            .HasMaxLength(200);
        
        builder.Property(hotel => hotel.Description)
            .HasConversion(description => description.Value, value => new Description(value))
            .HasMaxLength(2000);
        
        builder.OwnsOne(hotel => hotel.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Country).HasMaxLength(100);
            addressBuilder.Property(a => a.State).HasMaxLength(100);
            addressBuilder.Property(a => a.City).HasMaxLength(100);
            addressBuilder.Property(a => a.ZipCode).HasMaxLength(10);
            addressBuilder.Property(a => a.Street).HasMaxLength(200);
        });
        
        builder.Property(hotel => hotel.IsActive)
            .HasConversion(
                isActive => isActive.Value, 
                value => IsActive.FromValue(value))
            .HasDefaultValue(IsActive.Default);
        
        builder.Property(hotel => hotel.CreatedAtOnUtc)
            .HasDefaultValueSql("NOW()");
    }
}