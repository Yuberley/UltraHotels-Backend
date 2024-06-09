namespace HotelHub.Domain.SharedValueObjects;

public sealed record IsActive
{
    public static IsActive Yes = new(true);
    public static IsActive No = new(false);
    
    private IsActive(bool value)
    {
        Value = value;
    }
    
    public bool Value { get; }
    
    public static implicit operator bool(IsActive isActive) => isActive.Value;
}