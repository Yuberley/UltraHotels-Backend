namespace HotelHub.Domain.Users;

public record Role(string Value)
{
    private static readonly Role Admin = new("Admin");
    private static readonly Role User = new("User");
    
    public static Role FromValue(string value)
    {
        return value switch
        {
            "Admin" => Admin,
            "User" => User,
            _ => throw new ArgumentException($"Unknown role: {value}")
        };
    }
    
    public bool IsValid => All.Contains(this);
    
    public static readonly IReadOnlyCollection<Role> All = new[]
    {
        Admin,
        User
    };
}