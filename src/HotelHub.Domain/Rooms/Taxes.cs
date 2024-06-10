namespace HotelHub.Domain.Rooms;

public sealed record Taxes(float Value)
{
    public static Taxes FromValue(float value)
    {
        if (value < 0 || value > 100)
        {
            throw new ApplicationException("The Taxes value is invalid");
        }
        
        return new Taxes(value);
    }
    
}