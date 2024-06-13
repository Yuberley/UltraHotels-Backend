namespace HotelHub.Domain.Guests;

public sealed record DocumentType(string Value)
{
    private static readonly DocumentType Passport = new DocumentType("passport");
    private static readonly DocumentType IdentityCard = new DocumentType("identitycard");
    private static readonly DocumentType DrivingLicense = new DocumentType("drivinglicense");
    
    public static DocumentType Create(string value)
    {
        string lowerValue = value.ToLower();
        return All.FirstOrDefault(c => c.Value == lowerValue) ??
               throw new ApplicationException("The document type value is invalid");
    }
    
    public static readonly IReadOnlyCollection<DocumentType> All = new[]
    {
        Passport,
        IdentityCard,
        DrivingLicense
    };
}