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
                value => new Domain.Guests.Email(value))
            .HasMaxLength(100);
        builder.HasIndex(guest => guest.Email).IsUnique();
        
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
                gender => gender.ToString(),
                gender => (Gender)Enum.Parse(typeof(Gender), gender))
            .HasMaxLength(10);
        
        builder.Property(guest => guest.DocumentType)
            .HasConversion(
                documentType => documentType.ToString(),
                documentType => (DocumentType)Enum.Parse(typeof(DocumentType), documentType))
            .HasMaxLength(10);
        
        builder.Property(guest => guest.DocumentNumber)
            .HasConversion(
                documentNumber => documentNumber.Value,
                value => new DocumentNumber(value))
            .HasMaxLength(20);
        
        builder.Property(guest => guest.EmergencyContactPhoneNumber)
            .HasConversion(
                ecpn => ecpn.Value,
                value => PhoneNumber.Create(value))
            .HasMaxLength(15);
        
        builder.Property(guest => guest.EmergencyContactFullName)
            .HasConversion(
                ecfn => ecfn.Value,
                value => new EmergencyContactFullName(value))
            .HasMaxLength(200);
        
        builder.Property(guest => guest.CreatedAtOnUtc)
            .HasDefaultValueSql("NOW()");
        
        builder.HasOne<Booking>()
            .WithMany()
            .HasForeignKey(guest => guest.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}