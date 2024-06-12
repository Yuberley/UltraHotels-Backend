using HotelHub.Domain.Abstractions;

namespace HotelHub.Domain.Users;

public class UserErrors
{
    public static Error NotFound => new(
        "Users.NotFound", 
        "The user was not found");
    
    public static Error InvalidCredentials => new(
        "Users.InvalidCredentials",
        "Invalid credentials");
    
    public static Error EmailAlreadyExists(string email) => new(
        "Users.EmailAlreadyExists",
        $"The email '{email}' is already in use");
    
    public static Error RoleNotFound => new(
        "Users.RoleNotFound",
        "The role was not found");
}