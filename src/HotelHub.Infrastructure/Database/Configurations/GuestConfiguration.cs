using HotelHub.Domain.Bookings;
using HotelHub.Domain.Guests;
using HotelHub.Domain.SharedValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Database.Configurations;

internal sealed class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("guests");
        
        builder.HasKey(guest => guest.Id);
        
        builder.Property(guest => guest.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                value => new FirstName(value))
            .HasMaxLength(200);
        
        builder.Property(guest => guest.LastName)
            .HasConversion(
                lastName => lastName.Value,
                value => new LastName(value))
            .HasMaxLength(200);
        
        builder.Property(guest => guest.Email)
            .HasConversion(
                email => email.Value,
                value => new Domain.SharedValueObjects.Email(value))
            .HasMaxLength(100);
        builder.HasIndex(guest => guest.Email);
        
        builder.Property(guest => guest.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value))
            .HasMaxLength(15);
        
        builder.Property(guest => guest.BirthDate)
            .HasConversion(
                birthDate => birthDate.Value,
                value => new BirthDate(value));
        
        builder.Property(guest => guest.Gender)
            .HasConversion(
                gender => gender.Value,
                gender => Gender.Create(gender))
            .HasMaxLength(30);
        
        builder.Property(guest => guest.DocumentType)
            .HasConversion(
                documentType => documentType.Value,
                documentType => DocumentType.Create(documentType))
            .HasMaxLength(40);
        
        builder.Property(guest => guest.DocumentNumber)
            .HasConversion(
                documentNumber => documentNumber.Value,
                value => new DocumentNumber(value))
            .HasMaxLength(50);
        
       builder.Property(guest => guest.CreatedAtOnUtc)
            .HasDefaultValueSql("NOW()");
        
        builder.HasOne<Booking>()
            .WithMany()
            .HasForeignKey(guest => guest.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}