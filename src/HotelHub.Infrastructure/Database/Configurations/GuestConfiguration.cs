using HotelHub.Domain.Guests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Database.Configurations;

internal sealed class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasKey(guest => guest.Id);
        
        builder.Property(guest => guest.FirstName)
            .HasMaxLength(200);
        
        builder.Property(guest => guest.LastName)
            .HasMaxLength(200);
        
        builder.Property(guest => guest.Email)
            .HasMaxLength(200);
        
        builder.Property(guest => guest.EmergencyContactFullName)
            .HasMaxLength(200);
    }
}