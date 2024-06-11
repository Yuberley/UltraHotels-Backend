namespace HotelHub.Domain.Rooms;

// public sealed record Taxes(float Value)
// {
//     public static Taxes FromValue(float value)
//     {
//         if (value < 0 || value > 100)
//         {
//             throw new ApplicationException("The Taxes value is invalid");
//         }
//         
//         return new Taxes(value);
//     }
//     
// }

public sealed record Taxes
{
    public float Value { get; }
    
    private Taxes(float value)
    {
        if (value is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "The Taxes value must be between 0 and 100.");
        }
        
        Value = value;
    }
    
    public static Taxes FromValue(float value) => new Taxes(value);
}