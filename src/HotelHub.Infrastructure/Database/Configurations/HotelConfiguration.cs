using HotelHub.Domain.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Database.Configurations;

internal sealed class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(hotel => hotel.Id);
        
        builder.Property(hotel => hotel.Name)
            .HasMaxLength(200);
        
        builder.Property(hotel => hotel.Description)
            .HasMaxLength(2000);
        
        builder.OwnsOne(hotel => hotel.Address);
        
        builder.Property(hotel => hotel.IsActive);
  
    }
}