using HotelHub.Domain.Abstractions;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Domain.Guests;

public sealed class Guest : Entity
{
    public Guest (
        Guid guestId,
        Guid bookingId,
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        BirthDate birthDate,
        Gender gender,
        DocumentType documentType,
        DocumentNumber documentNumber,
        DateTime createdAtOnUtc
    ) : base(guestId)
    {
        BookingId = bookingId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Gender = gender;
        DocumentType = documentType;
        DocumentNumber = documentNumber;
        CreatedAtOnUtc = createdAtOnUtc;
    }
    
    // This empty constructor is necessary for Entity Framework,
    // they require a constructor without parameters for instance creation.
    private Guest() {}
    
    public Guid BookingId { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public BirthDate BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public DocumentType DocumentType { get; private set; }
    public DocumentNumber DocumentNumber { get; private set; }
    public DateTime CreatedAtOnUtc { get; private set; }
}