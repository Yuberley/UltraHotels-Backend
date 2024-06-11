namespace HotelHub.Domain.SharedValueObjects;

public sealed record IsActive
{
    public static readonly IsActive Yes = new(true);
    public static readonly IsActive No = new(false);
    
    
    private IsActive(bool value) => Value = value;
    
    public bool Value { get; }
    
    public static IsActive Default => Yes;
    
    public static IsActive Assign(bool value) => new IsActive(value);
}