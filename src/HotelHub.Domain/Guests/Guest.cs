using HotelHub.Domain.Abstractions;

namespace HotelHub.Domain.Guests;

public sealed  class Guest : Entity
{
    public Guest (
        Guid guestId,
        Guid bookingId,
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        BirthDate birthDate,
        Gender gerder,
        DocumentType documentType,
        DocumentNumber documentNumber,
        EmergencyContactFullName emergencyContactFullName,
        PhoneNumber emergencyContactPhoneNumber,
        DateTime createdAtOnUtc
    ) : base(guestId)
    {
        BookingId = bookingId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Gerder = gerder;
        DocumentType = documentType;
        DocumentNumber = documentNumber;
        EmergencyContactFullName = emergencyContactFullName;
        EmergencyContactPhoneNumber = emergencyContactPhoneNumber;
        CreatedAtOnUtc = createdAtOnUtc;
    }
    
    public Guid BookingId { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public BirthDate BirthDate { get; private set; }
    public Gender Gerder { get; private set; }
    public DocumentType DocumentType { get; private set; }
    public DocumentNumber DocumentNumber { get; private set; }
    public EmergencyContactFullName EmergencyContactFullName { get; private set; }
    public PhoneNumber EmergencyContactPhoneNumber { get; private set; }
    public DateTime CreatedAtOnUtc { get; private set; }
    
}