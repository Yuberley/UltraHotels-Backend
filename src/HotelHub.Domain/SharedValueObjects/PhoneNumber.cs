using System.Text.RegularExpressions;

namespace HotelHub.Domain.SharedValueObjects;

public partial record PhoneNumber
{
    // The phone number must start with a +
    // The phone number must have at least 7 digits and at most 15 digits (excluding the +)
    private const string PhoneNumberPattern = @"^\+[1-9]{1}[0-9]{6,14}$";
    private PhoneNumber(string value) => Value = value;
    
    public string Value { get; }
    
    public static PhoneNumber Create(string value)
    {
        if (!NumberPhoneRegex().IsMatch(value))
        {
            throw new ApplicationException("The phone number is invalid");
        }
        
        return new PhoneNumber(value);
    }

    [GeneratedRegex(PhoneNumberPattern)]
    private static partial Regex NumberPhoneRegex();
}